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
    : IGtfsArchiveMapper<FareLegRuleImportDto, FareLegRule>, IGtfsCrudMapper<FareLegRule, CreateFareLegRuleDto, UpdateFareLegRuleDto, FareLegRuleDto,
        FareLegRuleCreateModel, FareLegRuleUpdateModel>
{
    public partial FareLegRuleCreateModel Map(CreateFareLegRuleDto input);
    public partial FareLegRuleUpdateModel Map(UpdateFareLegRuleDto input);
    public partial FareLegRuleDto Map(FareLegRule entity);

    // CSV satirini entity'ye esler.
    public partial FareLegRule Map(FareLegRuleImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial FareLegRuleImportDto MapToImport(FareLegRule entity);
}
