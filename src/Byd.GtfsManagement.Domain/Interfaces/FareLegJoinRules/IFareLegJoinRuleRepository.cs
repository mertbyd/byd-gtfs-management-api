using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareLegJoinRules;

namespace Byd.GtfsManagement.Interfaces.FareLegJoinRules;

// islevi: FareLegJoinRule entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret etap birlestirme kurali sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret etap birlestirme kurali repository sozlesmesini tanimlar.</summary>
public interface IFareLegJoinRuleRepository : IExtendedRepository<FareLegJoinRule>
{
}
