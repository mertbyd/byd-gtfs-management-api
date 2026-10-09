using Byd.GtfsManagement.Dtos.FareTransferRules;

namespace Byd.GtfsManagement.Services.FareTransferRules;

// islevi: FareTransferRule CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret aktarma kurali CRUD API sozlesmesini bildirir.</summary>
public interface IFareTransferRuleAppService
    : IGtfsCrudAppService<CreateFareTransferRuleDto, UpdateFareTransferRuleDto, FareTransferRuleDto, FareTransferRuleFilter>
{
}
