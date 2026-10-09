using Byd.GtfsManagement.Dtos.FareMedias;
using Byd.GtfsManagement.Entities.FareMedias;
using Byd.GtfsManagement.Models.FareMedias;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareMedias;

// islevi: FareMedia DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret medyasi DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareMediaMapper
    : IGtfsCrudMapper<FareMedia, CreateFareMediaDto, UpdateFareMediaDto, FareMediaDto,
        FareMediaCreateModel, FareMediaUpdateModel>
{
    public partial FareMediaCreateModel Map(CreateFareMediaDto input);
    public partial FareMediaUpdateModel Map(UpdateFareMediaDto input);
    public partial FareMediaDto Map(FareMedia entity);
}
