using Byd.GtfsManagement.Dtos.FareRules;
using Byd.GtfsManagement.Entities.FareRules;
using Byd.GtfsManagement.Models.FareRules;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareRules;

// islevi: FareRule DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret kurali DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareRuleMapper
    : IGtfsCrudMapper<FareRule, CreateFareRuleDto, UpdateFareRuleDto, FareRuleDto,
        FareRuleCreateModel, FareRuleUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial FareRuleCreateModel Map(CreateFareRuleDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial FareRuleUpdateModel Map(UpdateFareRuleDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial FareRuleDto Map(FareRule entity);
}
