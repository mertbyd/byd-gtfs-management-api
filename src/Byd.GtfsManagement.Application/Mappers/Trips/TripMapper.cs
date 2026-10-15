using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Models.Trips;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Trips;

// islevi: Trip domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class TripMapper
    : IGtfsArchiveMapper<TripImportDto, Trip>, IGtfsCrudMapper<Trip, CreateTripDto, UpdateTripDto, TripDto, TripCreateModel, TripUpdateModel>
{
    public partial TripCreateModel Map(CreateTripDto input);
    public partial TripUpdateModel Map(UpdateTripDto input);
    public partial TripDto Map(Trip entity);
    public partial TripDetailDto Map(TripDetailModel model);

    // CSV satirini entity'ye esler.
    public partial Trip Map(TripImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial TripImportDto MapToImport(Trip entity);
}
