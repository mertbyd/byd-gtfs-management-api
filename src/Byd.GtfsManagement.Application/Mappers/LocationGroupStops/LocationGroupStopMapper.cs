using Byd.GtfsManagement.Dtos.LocationGroupStops;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Byd.GtfsManagement.Models.LocationGroupStops;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.LocationGroupStops;

// islevi: LocationGroupStop DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS konum grubu durak iliskisi DTO ve modellerini esler.</summary>
[Mapper]
public partial class LocationGroupStopMapper
    : IGtfsArchiveMapper<LocationGroupStopImportDto, LocationGroupStop>, IGtfsCrudMapper<LocationGroupStop, CreateLocationGroupStopDto, UpdateLocationGroupStopDto, LocationGroupStopDto,
        LocationGroupStopCreateModel, LocationGroupStopUpdateModel>
{
    public partial LocationGroupStopCreateModel Map(CreateLocationGroupStopDto input);
    public partial LocationGroupStopUpdateModel Map(UpdateLocationGroupStopDto input);
    public partial LocationGroupStopDto Map(LocationGroupStop entity);

    // CSV satirini entity'ye esler.
    public partial LocationGroupStop Map(LocationGroupStopImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial LocationGroupStopImportDto MapToImport(LocationGroupStop entity);
}
