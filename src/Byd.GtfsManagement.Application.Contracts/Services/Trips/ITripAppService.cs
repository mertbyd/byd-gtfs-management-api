using Byd.GtfsManagement.Dtos.Trips;

namespace Byd.GtfsManagement.Services.Trips;

// islevi: trips.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface ITripAppService
    : IGtfsCrudAppService<CreateTripDto, UpdateTripDto, TripDto, TripFilter>
{
}
