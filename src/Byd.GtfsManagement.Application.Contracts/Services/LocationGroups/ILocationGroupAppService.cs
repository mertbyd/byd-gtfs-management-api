using Byd.GtfsManagement.Dtos.LocationGroups;

namespace Byd.GtfsManagement.Services.LocationGroups;

// islevi: LocationGroup CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS konum grubu CRUD API sozlesmesini bildirir.</summary>
public interface ILocationGroupAppService
    : IGtfsCrudAppService<CreateLocationGroupDto, UpdateLocationGroupDto, LocationGroupDto, LocationGroupFilter>
{
}
