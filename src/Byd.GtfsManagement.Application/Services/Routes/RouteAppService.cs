using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Managers.Routes;
using Byd.GtfsManagement.Mappers.Routes;
using Byd.GtfsManagement.Models.Routes;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: Route kayitlarinin CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class RouteAppService
    : GtfsCrudAppServiceBase<
        Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter,
        RouteCreateModel, RouteUpdateModel, RouteManager, IRouteRepository, RouteMapper>, IRouteAppService
{
}
