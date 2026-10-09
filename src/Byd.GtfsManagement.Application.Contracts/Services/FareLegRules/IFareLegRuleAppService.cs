using Byd.GtfsManagement.Dtos.FareLegRules;

namespace Byd.GtfsManagement.Services.FareLegRules;

// islevi: FareLegRule CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret etap kurali CRUD API sozlesmesini bildirir.</summary>
public interface IFareLegRuleAppService
    : IGtfsCrudAppService<CreateFareLegRuleDto, UpdateFareLegRuleDto, FareLegRuleDto, FareLegRuleFilter>
{
}
