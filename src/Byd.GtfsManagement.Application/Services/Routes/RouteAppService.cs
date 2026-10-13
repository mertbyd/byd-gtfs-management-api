using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Mappers.Routes;
using Byd.GtfsManagement.Models.Routes;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: Route kayitlarinin CRUD akisini ortak tabana baglar; kaydi sefer kaliplariyla okuyan detay ucunu sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD tabanda kalir, detay kurali RouteManager'dadir.
public class RouteAppService
    : GtfsCrudAppServiceBase<
        Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter,
        RouteCreateModel, RouteUpdateModel, RouteManager, IRouteRepository, RouteMapper>, IRouteAppService
{
    // Kaydi sefer kaliplariyla birlikte getirir.
    public virtual async Task<RouteDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetDetailAsync(id, cancellationToken));
}
