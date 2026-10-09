using Byd.GtfsManagement.Dtos.FareRules;
using Byd.GtfsManagement.Entities.FareRules;
using Byd.GtfsManagement.Interfaces.FareRules;
using Byd.GtfsManagement.Managers.FareRules;
using Byd.GtfsManagement.Mappers.FareRules;
using Byd.GtfsManagement.Models.FareRules;

namespace Byd.GtfsManagement.Services.FareRules;

// islevi: FareRule CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret kurali CRUD servisini ortak tabana baglar.</summary>
public class FareRuleAppService
    : GtfsCrudAppServiceBase<
        FareRule, CreateFareRuleDto, UpdateFareRuleDto, FareRuleDto, FareRuleFilter,
        FareRuleCreateModel, FareRuleUpdateModel, FareRuleManager, IFareRuleRepository, FareRuleMapper>,
        IFareRuleAppService
{
}
