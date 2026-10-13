using Byd.GtfsManagement.Dtos.LocationGroupStops;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Byd.GtfsManagement.Interfaces.LocationGroupStops;
using Byd.GtfsManagement.Managers.LocationGroupStops;
using Byd.GtfsManagement.Mappers.LocationGroupStops;
using Byd.GtfsManagement.Models.LocationGroupStops;

namespace Byd.GtfsManagement.Services.LocationGroupStops;

// islevi: LocationGroupStop CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS konum grubu durak iliskisi CRUD servisini ortak tabana baglar.</summary>
public class LocationGroupStopAppService
    : GtfsCrudAppServiceBase<
        LocationGroupStop, CreateLocationGroupStopDto, UpdateLocationGroupStopDto, LocationGroupStopDto, LocationGroupStopFilter,
        LocationGroupStopCreateModel, LocationGroupStopUpdateModel, LocationGroupStopManager, ILocationGroupStopRepository, LocationGroupStopMapper>,
        ILocationGroupStopAppService
{
}
