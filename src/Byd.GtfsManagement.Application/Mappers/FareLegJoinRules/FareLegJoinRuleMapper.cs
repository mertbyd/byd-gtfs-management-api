using Byd.GtfsManagement.Dtos.FareLegJoinRules;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Byd.GtfsManagement.Models.FareLegJoinRules;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FareLegJoinRules;

// islevi: FareLegJoinRule DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ucret etap birlestirme kurali DTO ve modellerini esler.</summary>
[Mapper]
public partial class FareLegJoinRuleMapper
    : IGtfsArchiveMapper<FareLegJoinRuleImportDto, FareLegJoinRule>, IGtfsCrudMapper<FareLegJoinRule, CreateFareLegJoinRuleDto, UpdateFareLegJoinRuleDto, FareLegJoinRuleDto,
        FareLegJoinRuleCreateModel, FareLegJoinRuleUpdateModel>
{
    public partial FareLegJoinRuleCreateModel Map(CreateFareLegJoinRuleDto input);
    public partial FareLegJoinRuleUpdateModel Map(UpdateFareLegJoinRuleDto input);
    public partial FareLegJoinRuleDto Map(FareLegJoinRule entity);

    // CSV satirini entity'ye esler.
    public partial FareLegJoinRule Map(FareLegJoinRuleImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial FareLegJoinRuleImportDto MapToImport(FareLegJoinRule entity);
}
