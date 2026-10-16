using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Interfaces;
using FluentValidation;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Mappers.Stops;
using Byd.GtfsManagement.Models.Stops;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Services.Stops;

// islevi: Stop kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar; harita, detay ve kalkis okumalarini sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD ve arsiv akisi tabanda kalir, okuma kurallari StopManager'dadir.
public class StopAppService
    : GtfsImportExportAppServiceBase<
        Stop, CreateStopDto, UpdateStopDto, StopDto, StopFilter,
        StopCreateModel, StopUpdateModel, StopManager, IStopRepository, StopMapper, StopImportDto>, IStopAppService
{
    private IStopRepository StopRepository => LazyGetRequiredService<IStopRepository>();

    // Liste girdisini dogrular ve filtreyi organizasyonun aktif Feed durak indeksinde calistirir.
    public virtual async Task<PagedResultDto<StopDto>> SearchAsync(
        DataTableListInput<StopFilter> input, CancellationToken cancellationToken = default)
    {
        await Validator<DataTableListInput<StopFilter>>().ValidateAndThrowAsync(input, cancellationToken);
        var result = await StopRepository.SearchAsync(input, cancellationToken);
        return new PagedResultDto<StopDto>(result.TotalCount, result.Items.Select(Mapper.Map).ToList());
    }

    // Verilen ya da aktif Feed'in duraklarini harita icin getirir.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<List<StopDto>> GetMapAsync(Guid? feedId, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetMapAsync(feedId, cancellationToken));

    // Duragi alt peronlari, ugrayan guzergahlari, pathway ve transfer kayitlariyla getirir.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<StopDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetDetailAsync(id, cancellationToken));

    // Kalkis girdisini dogrular ve duraktan servis gunundeki kalkislari getirir.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<List<StopDepartureDto>> GetDeparturesAsync(
        Guid id, StopDepartureInputDto input, CancellationToken cancellationToken = default)
    {
        await Validator<StopDepartureInputDto>().ValidateAndThrowAsync(input, cancellationToken);
        return Mapper.Map(await Manager.GetDeparturesAsync(id, Mapper.Map(input), cancellationToken));
    }
    // Taslak veya gecmis detayini yetkili surum kapsaminda okur.
    public virtual async Task<StopDetailDto> GetDetailForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        Manager.EnsureInFeed(await Manager.EnsureExistsInScopeAsync(id, cancellationToken), feedId);
        return await GetDetailAsync(id, cancellationToken);
    }

    // Taslagin duraklarini yayin secimini degistirmeden haritada onizler.
    public virtual async Task<List<StopDto>> GetMapForFeedAsync(
        Guid feedId, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        return await GetMapAsync(feedId, cancellationToken);
    }

    // Taslagin sefer saatlerini yalniz secilen surumden getirir.
    public virtual async Task<List<StopDepartureDto>> GetDeparturesForFeedAsync(
        Guid feedId, Guid id, StopDepartureInputDto input, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        Manager.EnsureInFeed(await Manager.EnsureExistsInScopeAsync(id, cancellationToken), feedId);
        return await GetDeparturesAsync(id, input, cancellationToken);
    }
}
