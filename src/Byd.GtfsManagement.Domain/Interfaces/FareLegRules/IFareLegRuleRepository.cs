using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareLegRules;

namespace Byd.GtfsManagement.Interfaces.FareLegRules;

// islevi: FareLegRule entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret etap kurali sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret etap kurali repository sozlesmesini tanimlar.</summary>
public interface IFareLegRuleRepository : IExtendedRepository<FareLegRule>
{
}
