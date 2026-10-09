using Byd.GtfsManagement.Dtos.LocationGroups;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Models.LocationGroups;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.LocationGroups;

// islevi: LocationGroup DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS konum grubu DTO ve modellerini esler.</summary>
[Mapper]
public partial class LocationGroupMapper
    : IGtfsCrudMapper<LocationGroup, CreateLocationGroupDto, UpdateLocationGroupDto, LocationGroupDto,
        LocationGroupCreateModel, LocationGroupUpdateModel>
{
    public partial LocationGroupCreateModel Map(CreateLocationGroupDto input);
    public partial LocationGroupUpdateModel Map(UpdateLocationGroupDto input);
    public partial LocationGroupDto Map(LocationGroup entity);
}
