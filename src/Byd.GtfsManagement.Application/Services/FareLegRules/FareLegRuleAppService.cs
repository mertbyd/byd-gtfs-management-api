using Byd.GtfsManagement.Dtos.FareLegRules;
using Byd.GtfsManagement.Entities.FareLegRules;
using Byd.GtfsManagement.Interfaces.FareLegRules;
using Byd.GtfsManagement.Managers.FareLegRules;
using Byd.GtfsManagement.Mappers.FareLegRules;
using Byd.GtfsManagement.Models.FareLegRules;

namespace Byd.GtfsManagement.Services.FareLegRules;

// islevi: FareLegRule CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret etap kurali CRUD servisini ortak tabana baglar.</summary>
public class FareLegRuleAppService
    : GtfsCrudAppServiceBase<
        FareLegRule, CreateFareLegRuleDto, UpdateFareLegRuleDto, FareLegRuleDto, FareLegRuleFilter,
        FareLegRuleCreateModel, FareLegRuleUpdateModel, FareLegRuleManager, IFareLegRuleRepository, FareLegRuleMapper>,
        IFareLegRuleAppService
{
}
