using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Timeframes;

namespace Byd.GtfsManagement.Interfaces.Timeframes;

// islevi: Timeframe entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Zaman araligi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS zaman araligi repository sozlesmesini tanimlar.</summary>
public interface ITimeframeRepository : IExtendedRepository<Timeframe>
{
}
