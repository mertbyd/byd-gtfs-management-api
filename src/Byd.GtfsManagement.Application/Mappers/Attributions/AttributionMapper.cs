using Byd.GtfsManagement.Dtos.Attributions;
using Byd.GtfsManagement.Entities.Attributions;
using Byd.GtfsManagement.Models.Attributions;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Attributions;

// islevi: Attribution DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS atif DTO ve modellerini esler.</summary>
[Mapper]
public partial class AttributionMapper
    : IGtfsArchiveMapper<AttributionImportDto, Attribution>, IGtfsCrudMapper<Attribution, CreateAttributionDto, UpdateAttributionDto, AttributionDto,
        AttributionCreateModel, AttributionUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial AttributionCreateModel Map(CreateAttributionDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial AttributionUpdateModel Map(UpdateAttributionDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial AttributionDto Map(Attribution entity);

    // CSV satirini entity'ye esler.
    public partial Attribution Map(AttributionImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial AttributionImportDto MapToImport(Attribution entity);
}
