using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Mappers.Routes;
using Byd.GtfsManagement.Models.Routes;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: Route kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar; kaydi sefer kaliplariyla okuyan detay ucunu
// sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD ve arsiv akisi tabanda kalir, detay kurali RouteManager'dadir.
public class RouteAppService
    : GtfsImportExportAppServiceBase<
        Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter,
        RouteCreateModel, RouteUpdateModel, RouteManager, IRouteRepository, RouteMapper, RouteImportDto>, IRouteAppService
{
    // Kaydi sefer kaliplariyla birlikte getirir.
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
