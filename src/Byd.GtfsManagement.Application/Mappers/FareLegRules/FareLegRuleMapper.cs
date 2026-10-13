using Byd.GtfsManagement.Dtos.FareLegRules;
using Byd.GtfsManagement.Entities.FareLegRules;
using Byd.GtfsManagement.Models.FareLegRules;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareLegRules;

// islevi: FareLegRule DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret etap kurali DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareLegRuleMapper
    : IGtfsCrudMapper<FareLegRule, CreateFareLegRuleDto, UpdateFareLegRuleDto, FareLegRuleDto,
        FareLegRuleCreateModel, FareLegRuleUpdateModel>
{
    public partial FareLegRuleCreateModel Map(CreateFareLegRuleDto input);
    public partial FareLegRuleUpdateModel Map(UpdateFareLegRuleDto input);
    public partial FareLegRuleDto Map(FareLegRule entity);
}
