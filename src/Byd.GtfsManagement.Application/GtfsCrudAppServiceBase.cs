using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Managers;
using Byd.GtfsManagement.Mappers;
using Byd.GtfsManagement.Services;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement;

// islevi: Feed kapsamli GTFS kayitlarinin olustur, oku, listele, guncelle ve sil akisini tek yerde yurutur.
// sistemdeki gorevi: Ortak CRUD sozlesmesini uygular; tipli repository, Manager ve Mapperly baglarini da merkezde cozer.
// Yaprak servisler yalniz kendi okuma use case'lerini bu baglarla ekler.
public abstract class GtfsCrudAppServiceBase<
    TEntity, TCreateDto, TUpdateDto, TDto, TFilter,
    TCreateModel, TUpdateModel, TManager, TRepository, TMapper>
    : GtfsManagementAppService, IGtfsCrudAppService<TCreateDto, TUpdateDto, TDto, TFilter>
    where TEntity : class, IEntity<Guid>, IHasFeed
    where TFilter : class
    where TManager : GtfsFeedScopedManager<TEntity, TCreateModel, TUpdateModel>
    where TRepository : IExtendedRepository<TEntity>
    where TMapper : IGtfsCrudMapper<TEntity, TCreateDto, TUpdateDto, TDto, TCreateModel, TUpdateModel>, new()
{
    protected static readonly TMapper Mapper = new();

    protected TManager Manager => LazyGetRequiredService<TManager>();

    private IExtendedRepository<TEntity> Repository => LazyGetRequiredService<TRepository>();

    // Girdiyi dogrulayip yeni kaydi olusturur ve kalici hale getirir.
    public virtual async Task<TDto> CreateAsync(TCreateDto input, CancellationToken cancellationToken = default)
    {
        await Validator<TCreateDto>().ValidateAndThrowAsync(input, cancellationToken);
        var entity = await Manager.CreateAsync(Mapper.Map(input), cancellationToken);
        var saved = await Repository.InsertAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        return Mapper.Map(saved);
    }

    // Kaydi kapsam kuralindan gecirerek okur.
    public virtual async Task<TDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.EnsureExistsInScopeAsync(id, cancellationToken));

    // Liste girdisini dogrular, filtreli sayfayi ve toplam sayiyi tek sonuc olarak getirir.
    public virtual async Task<PagedResultDto<TDto>> GetFilteredListAsync(
        DataTableListInput<TFilter> input, CancellationToken cancellationToken = default)
    {
        await Validator<DataTableListInput<TFilter>>().ValidateAndThrowAsync(input, cancellationToken);
        var entities = await Repository.GetFilteredListAsync(input, cancellationToken);
        var totalCount = await Repository.GetFilteredCountAsync(input, cancellationToken);
        var items = entities.Select(Mapper.Map).ToList();
        return new PagedResultDto<TDto>(totalCount, items);
    }

    // Kapsamdaki kaydi yukleyip duzenlenebilir alanlarini gunceller.
    public virtual async Task<TDto> UpdateAsync(
        Guid id, TUpdateDto input, CancellationToken cancellationToken = default)
    {
        await Validator<TUpdateDto>().ValidateAndThrowAsync(input, cancellationToken);
        var entity = await Manager.EnsureExistsInScopeAsync(id, cancellationToken);
        await Manager.UpdateAsync(entity, Mapper.Map(input), cancellationToken);
        var saved = await Repository.UpdateAsync(entity, autoSave: true, cancellationToken: cancellationToken);
        return Mapper.Map(saved);
    }

    // Kapsamdaki kaydi, baska GTFS satirlarinca kullanilmiyorsa kalici olarak siler.
    public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Manager.EnsureExistsInScopeAsync(id, cancellationToken);
        await Manager.EnsureCanDeleteAsync(entity, cancellationToken);
        await Repository.DeleteAsync(entity, autoSave: true, cancellationToken: cancellationToken);
    }

    // Girdi tipinin FluentValidation dogrulayicisini kullanim aninda cozer.
    protected IValidator<TInput> Validator<TInput>() => LazyGetRequiredService<IValidator<TInput>>();
}
