using Byd.GtfsManagement.Dtos.FareLegJoinRules;

namespace Byd.GtfsManagement.Services.FareLegJoinRules;

// islevi: FareLegJoinRule CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ucret etap birlestirme kurali CRUD API sozlesmesini bildirir.</summary>
public interface IFareLegJoinRuleAppService
    : IGtfsCrudAppService<CreateFareLegJoinRuleDto, UpdateFareLegJoinRuleDto, FareLegJoinRuleDto, FareLegJoinRuleFilter>
{
}
