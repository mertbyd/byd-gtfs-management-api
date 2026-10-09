using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareRules;

namespace Byd.GtfsManagement.Interfaces.FareRules;

// islevi: FareRule entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret kurali sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret kurali repository sozlesmesini tanimlar.</summary>
public interface IFareRuleRepository : IExtendedRepository<FareRule>
{
}
