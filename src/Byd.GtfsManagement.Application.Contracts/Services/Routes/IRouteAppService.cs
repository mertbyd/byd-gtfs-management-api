using Byd.GtfsManagement.Dtos.Routes;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: routes.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IRouteAppService
    : IGtfsCrudAppService<CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter>
{
}
