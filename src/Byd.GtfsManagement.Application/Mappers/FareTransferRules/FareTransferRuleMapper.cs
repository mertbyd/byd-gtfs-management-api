using Byd.GtfsManagement.Dtos.FareTransferRules;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Byd.GtfsManagement.Models.FareTransferRules;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareTransferRules;

// islevi: FareTransferRule DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret aktarma kurali DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareTransferRuleMapper
    : IGtfsCrudMapper<FareTransferRule, CreateFareTransferRuleDto, UpdateFareTransferRuleDto, FareTransferRuleDto,
        FareTransferRuleCreateModel, FareTransferRuleUpdateModel>
{
    public partial FareTransferRuleCreateModel Map(CreateFareTransferRuleDto input);
    public partial FareTransferRuleUpdateModel Map(UpdateFareTransferRuleDto input);
    public partial FareTransferRuleDto Map(FareTransferRule entity);
}
