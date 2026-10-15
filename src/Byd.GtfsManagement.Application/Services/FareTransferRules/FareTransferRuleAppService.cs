using Byd.GtfsManagement.Dtos.FareTransferRules;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Byd.GtfsManagement.Interfaces.FareTransferRules;
using Byd.GtfsManagement.Managers.FareTransferRules;
using Byd.GtfsManagement.Mappers.FareTransferRules;
using Byd.GtfsManagement.Models.FareTransferRules;

namespace Byd.GtfsManagement.Services.FareTransferRules;

// islevi: FareTransferRule CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ucret aktarma kurali CRUD servisini ortak tabana baglar.</summary>
public class FareTransferRuleAppService
    : GtfsImportExportAppServiceBase<
        FareTransferRule, CreateFareTransferRuleDto, UpdateFareTransferRuleDto, FareTransferRuleDto, FareTransferRuleFilter,
        FareTransferRuleCreateModel, FareTransferRuleUpdateModel, FareTransferRuleManager, IFareTransferRuleRepository, FareTransferRuleMapper, FareTransferRuleImportDto>,
        IFareTransferRuleAppService
{
}
