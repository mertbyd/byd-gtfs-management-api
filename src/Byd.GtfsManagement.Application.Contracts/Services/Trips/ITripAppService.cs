using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Trips;

namespace Byd.GtfsManagement.Services.Trips;

// islevi: trips.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir; kaydi duraklari ve takvimiyle
// birlikte okuyan detay ucunu ekler.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface ITripAppService
    : IGtfsCrudAppService<CreateTripDto, UpdateTripDto, TripDto, TripFilter>
{
    Task<TripDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TripDetailDto> GetDetailForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default);
}
