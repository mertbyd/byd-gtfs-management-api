using Byd.GtfsManagement.Dtos.FareRules;

namespace Byd.GtfsManagement.Services.FareRules;

// islevi: FareRule CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret kurali CRUD API sozlesmesini bildirir.</summary>
public interface IFareRuleAppService
    : IGtfsCrudAppService<CreateFareRuleDto, UpdateFareRuleDto, FareRuleDto, FareRuleFilter>
{
}
