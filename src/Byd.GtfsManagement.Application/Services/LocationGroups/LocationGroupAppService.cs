using Byd.GtfsManagement.Dtos.LocationGroups;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Interfaces.LocationGroups;
using Byd.GtfsManagement.Managers.LocationGroups;
using Byd.GtfsManagement.Mappers.LocationGroups;
using Byd.GtfsManagement.Models.LocationGroups;

namespace Byd.GtfsManagement.Services.LocationGroups;

// islevi: LocationGroup CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS konum grubu CRUD servisini ortak tabana baglar.</summary>
public class LocationGroupAppService
    : GtfsCrudAppServiceBase<
        LocationGroup, CreateLocationGroupDto, UpdateLocationGroupDto, LocationGroupDto, LocationGroupFilter,
        LocationGroupCreateModel, LocationGroupUpdateModel, LocationGroupManager, ILocationGroupRepository, LocationGroupMapper>,
        ILocationGroupAppService
{
}
