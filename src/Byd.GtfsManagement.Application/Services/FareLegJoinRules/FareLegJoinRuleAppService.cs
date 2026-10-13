using Byd.GtfsManagement.Dtos.FareLegJoinRules;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Byd.GtfsManagement.Interfaces.FareLegJoinRules;
using Byd.GtfsManagement.Managers.FareLegJoinRules;
using Byd.GtfsManagement.Mappers.FareLegJoinRules;
using Byd.GtfsManagement.Models.FareLegJoinRules;

namespace Byd.GtfsManagement.Services.FareLegJoinRules;

// islevi: FareLegJoinRule CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret etap birlestirme kurali CRUD servisini ortak tabana baglar.</summary>
public class FareLegJoinRuleAppService
    : GtfsCrudAppServiceBase<
        FareLegJoinRule, CreateFareLegJoinRuleDto, UpdateFareLegJoinRuleDto, FareLegJoinRuleDto, FareLegJoinRuleFilter,
        FareLegJoinRuleCreateModel, FareLegJoinRuleUpdateModel, FareLegJoinRuleManager, IFareLegJoinRuleRepository, FareLegJoinRuleMapper>,
        IFareLegJoinRuleAppService
{
}
