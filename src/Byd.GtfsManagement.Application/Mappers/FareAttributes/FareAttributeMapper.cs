using Byd.GtfsManagement.Dtos.FareAttributes;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.Models.FareAttributes;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareAttributes;

// islevi: FareAttribute DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret ozelligi DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareAttributeMapper
    : IGtfsArchiveMapper<FareAttributeImportDto, FareAttribute>, IGtfsCrudMapper<FareAttribute, CreateFareAttributeDto, UpdateFareAttributeDto, FareAttributeDto,
        FareAttributeCreateModel, FareAttributeUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial FareAttributeCreateModel Map(CreateFareAttributeDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial FareAttributeUpdateModel Map(UpdateFareAttributeDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial FareAttributeDto Map(FareAttribute entity);

    // CSV satirini entity'ye esler.
    public partial FareAttribute Map(FareAttributeImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial FareAttributeImportDto MapToImport(FareAttribute entity);
}
