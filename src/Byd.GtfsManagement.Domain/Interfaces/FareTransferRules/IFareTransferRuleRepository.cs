using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FareTransferRules;

namespace Byd.GtfsManagement.Interfaces.FareTransferRules;

// islevi: FareTransferRule entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ucret aktarma kurali sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ucret aktarma kurali repository sozlesmesini tanimlar.</summary>
public interface IFareTransferRuleRepository : IExtendedRepository<FareTransferRule>
{
}
