using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Routes;
using Volo.Abp.Application.Dtos;

namespace Byd.GtfsManagement.Services.Routes;

// islevi: routes.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir; arama ucunu ve
// kaydi sefer kaliplariyla birlikte okuyan detay ucunu ekler.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IRouteAppService
    : IGtfsCrudAppService<CreateRouteDto, UpdateRouteDto, RouteDto, RouteFilter>
{
    Task<PagedResultDto<RouteDto>> SearchAsync(
        DataTableListInput<RouteFilter> input, CancellationToken cancellationToken = default);
    Task<RouteDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RouteDetailDto> GetDetailForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default);
}
