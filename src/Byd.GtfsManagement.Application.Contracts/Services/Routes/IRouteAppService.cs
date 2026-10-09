using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Routes;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: routes.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir; kaydi sefer kaliplariyla
// birlikte okuyan detay ucunu ekler.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IRouteAppService
    : IGtfsCrudAppService<CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter>
{
    Task<RouteDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
}
