using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Managers;
using Byd.GtfsManagement.Mappers;
using Byd.GtfsManagement.Services;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Entities.Feeds;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;

namespace Byd.GtfsManagement;

// islevi: Feed kapsamli GTFS kayitlarinin olustur, oku, listele, guncelle ve sil akisini tek yerde yurutur; kalici her
// degisiklikten sonra Feed'in GTFS verisini yeniden dogrulatir.
// sistemdeki gorevi: Ortak CRUD sozlesmesini uygular; tipli repository, Manager ve Mapperly baglarini da merkezde cozerek
// yaprak servisleri bos tutar. Degisiklik ve rapor ayni islemde yazilir; dogrulayiciya ulasilamazsa degisiklik geri alinir.
public abstract class GtfsCrudAppServiceBase<
    TEntity, TCreateDto, TUpdateDto, TDto, TFilter,
    TCreateModel, TUpdateModel, TManager, TRepository, TMapper>
    : GtfsManagementAppService, IGtfsCrudAppService<TCreateDto, TUpdateDto, TDto, TFilter>
    where TEntity : class, IGtfsEntity
    where TFilter : class, IFeedFilter, new()
    where TManager : GtfsFeedScopedManager<TEntity, TCreateModel, TUpdateModel>
    where TRepository : IExtendedRepository<TEntity>
    where TMapper : IGtfsCrudMapper<TEntity, TCreateDto, TUpdateDto, TDto, TCreateModel, TUpdateModel>, new()
{
    protected static readonly TMapper Mapper = new();

    protected TManager Manager => LazyGetRequiredService<TManager>();

    private IExtendedRepository<TEntity> Repository => LazyGetRequiredService<TRepository>();

    protected FeedManager FeedManager => LazyGetRequiredService<FeedManager>();

    private IGtfsFeedValidator FeedValidator => LazyGetRequiredService<IGtfsFeedValidator>();
    private IDistributedEventBus DistributedEventBus => LazyGetRequiredService<IDistributedEventBus>();
    private ICachedResponseStore CachedResponseStore => LazyGetRequiredService<ICachedResponseStore>();
    // Girdi tipinin FluentValidation dogrulayicisini kullanim aninda cozer.
    protected IValidator<TInput> Validator<TInput>() => LazyGetRequiredService<IValidator<TInput>>();

    // Girdiyi dogrulayip yeni kaydi olusturur, kalici hale getirir, Feed'i yeniden dogrulatir ve okuma onbellegini temizler.
    public virtual async Task<TDto> CreateAsync(TCreateDto input, CancellationToken cancellationToken = default)
    {
        await Validator<TCreateDto>().ValidateAndThrowAsync(input, cancellationToken);
        var entity = await Manager.CreateAsync(Mapper.Map(input), cancellationToken);
        await FeedManager.EnsureEditableAsync(entity.FeedId, cancellationToken);
        await FeedValidator.ValidateAsync(
            () => Repository.InsertAsync(entity, autoSave: true, cancellationToken: cancellationToken),
            saved => saved.FeedId, cancellationToken);
        await CachedResponseStore.RemoveGroupAsync(FeedConsts.ResponseCacheGroup, cancellationToken);
        return Mapper.Map(entity);
    }

    // Kaydi kapsam kuralindan gecirerek okur.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<TDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.EnsureExistsInScopeAsync(id, cancellationToken));

    // Gecmis veya taslagin kaydini acik surum kimligiyle okur; tenant ve organizasyon korunur.
    public virtual async Task<TDto> GetForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        var entity = await Manager.EnsureExistsInScopeAsync(id, cancellationToken);
        Manager.EnsureInFeed(entity, feedId);
        return Mapper.Map(entity);
    }

    // Yonetim listesinde istemci filtresi hangi degeri tasirsa tasisin hedef surume sinirlar.
    public virtual async Task<PagedResultDto<TDto>> GetFilteredListForFeedAsync(
        Guid feedId, DataTableListInput<TFilter> input, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        Manager.ScopeFilter(input.Filters ??= new TFilter(), feedId);
        return await GetFilteredListAsync(input, cancellationToken);
    }

    // Liste girdisini dogrular, filtreli sayfayi ve toplam sayiyi tek sonuc olarak getirir.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<PagedResultDto<TDto>> GetFilteredListAsync(
        DataTableListInput<TFilter> input, CancellationToken cancellationToken = default)
    {
        await Validator<DataTableListInput<TFilter>>().ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        var totalCount = await Repository.GetFilteredCountAsync(input, cancellationToken);
        var items = entities.Select(Mapper.Map).ToList();
        return new PagedResultDto<TDto>(totalCount, items);
    }

    // Kapsamdaki kaydin duzenlenebilir alanlarini gunceller, Feed'i yeniden dogrulatir ve okuma onbellegini temizler.
    public virtual async Task<TDto> UpdateAsync(
        Guid id, TUpdateDto input, CancellationToken cancellationToken = default)
    {
        await Validator<TUpdateDto>().ValidateAndThrowAsync(input, cancellationToken);
        var entity = await Manager.EnsureExistsInScopeAsync(id, cancellationToken);
        var feed = await FeedManager.EnsureEditableAsync(entity.FeedId, cancellationToken);
        await FeedValidator.ValidateAsync(async () =>
        {
            await Manager.UpdateAsync(entity, Mapper.Map(input), cancellationToken);
            return await Repository.UpdateAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        }, saved => saved.FeedId, cancellationToken);
        await PublishFeedChangedAsync(feed);
        await CachedResponseStore.RemoveGroupAsync(FeedConsts.ResponseCacheGroup, cancellationToken);
        return Mapper.Map(entity);
    }

    // Kapsamdaki kaydi, baska GTFS satirlarinca kullanilmiyorsa kalici olarak siler, Feed'i yeniden dogrulatir ve okuma
    // onbellegini temizler.
    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Manager.EnsureExistsInScopeAsync(id, cancellationToken);
        var feed = await FeedManager.EnsureEditableAsync(entity.FeedId, cancellationToken);
        await Manager.EnsureCanDeleteAsync(entity, cancellationToken);
        await FeedValidator.ValidateAsync(async () =>
        {
            await Repository.DeleteAsync(entity, autoSave: true, cancellationToken: cancellationToken);
            return entity;
        }, deleted => deleted.FeedId, cancellationToken);
        await PublishFeedChangedAsync(feed);
        await CachedResponseStore.RemoveGroupAsync(FeedConsts.ResponseCacheGroup, cancellationToken);
    }

    // Basarili satir degisikligini ayni unit of work icinde diger modullere bildirir.
    private Task PublishFeedChangedAsync(Feed feed)
        => DistributedEventBus.PublishAsync(new FeedChangedEto
        {
            TenantId = feed.TenantId!.Value,
            OrganizationId = feed.OrganizationId!.Value,
            FeedId = feed.Id
        });

}
