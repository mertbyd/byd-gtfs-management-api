using Byd.GtfsManagement.Dtos.LocationGroupStops;

namespace Byd.GtfsManagement.Services.LocationGroupStops;

// islevi: LocationGroupStop CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS konum grubu durak iliskisi CRUD API sozlesmesini bildirir.</summary>
public interface ILocationGroupStopAppService
    : IGtfsCrudAppService<CreateLocationGroupStopDto, UpdateLocationGroupStopDto, LocationGroupStopDto, LocationGroupStopFilter>
{
}
