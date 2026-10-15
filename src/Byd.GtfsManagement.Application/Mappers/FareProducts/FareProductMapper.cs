using Byd.GtfsManagement.Dtos.FareProducts;
using Byd.GtfsManagement.Entities.FareProducts;
using Byd.GtfsManagement.Models.FareProducts;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareProducts;

// islevi: FareProduct DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret urunu DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareProductMapper
    : IGtfsArchiveMapper<FareProductImportDto, FareProduct>, IGtfsCrudMapper<FareProduct, CreateFareProductDto, UpdateFareProductDto, FareProductDto,
        FareProductCreateModel, FareProductUpdateModel>
{
    public partial FareProductCreateModel Map(CreateFareProductDto input);
    public partial FareProductUpdateModel Map(UpdateFareProductDto input);
    public partial FareProductDto Map(FareProduct entity);

    // CSV satirini entity'ye esler.
    public partial FareProduct Map(FareProductImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial FareProductImportDto MapToImport(FareProduct entity);
}
