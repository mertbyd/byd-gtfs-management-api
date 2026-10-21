using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Mappers.Routes;
using Byd.GtfsManagement.Models.Routes;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: Route kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar; aramayi ve kaydi sefer kaliplariyla okuyan
// detay ucunu sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD ve arsiv akisi tabanda kalir, detay kurali RouteManager'dadir.
public class RouteAppService
    : GtfsImportExportAppServiceBase<
        Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter,
        RouteCreateModel, RouteUpdateModel, RouteManager, IRouteRepository, RouteMapper, RouteImportDto>, IRouteAppService
{
    private IRouteRepository RouteRepository => LazyGetRequiredService<IRouteRepository>();

    // Liste girdisini dogrular ve filtreyi organizasyonun aktif Feed guzergah indeksinde calistirir.
    public virtual async Task<PagedResultDto<RouteDto>> SearchAsync(
        DataTableListInput<RouteFilter> input, CancellationToken cancellationToken = default)
    {
        await Validator<DataTableListInput<RouteFilter>>().ValidateAndThrowAsync(input, cancellationToken);
        var result = await RouteRepository.SearchAsync(input, cancellationToken);
        return new PagedResultDto<RouteDto>(result.TotalCount, result.Items.Select(Mapper.Map).ToList());
    }

    // Kaydi sefer kaliplariyla birlikte getirir.
    [CachedResponse(FeedConsts.ResponseCacheGroup)]
    public virtual async Task<RouteDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetDetailAsync(id, cancellationToken));
    // Taslak veya gecmis detayini yetkili surum kapsaminda okur.
    public virtual async Task<RouteDetailDto> GetDetailForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default)
    {
        using var draftRead = DataFilter.Disable<ActiveFeedFilter>();
        await FeedManager.EnsureExistsInScopeAsync(feedId, cancellationToken);
        Manager.EnsureInFeed(await Manager.EnsureExistsInScopeAsync(id, cancellationToken), feedId);
        return await GetDetailAsync(id, cancellationToken);
    }
}
