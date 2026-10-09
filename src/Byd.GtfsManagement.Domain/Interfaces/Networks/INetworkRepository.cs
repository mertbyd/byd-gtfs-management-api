using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Networks;

namespace Byd.GtfsManagement.Interfaces.Networks;

// islevi: Network entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Ulasim agi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS ulasim agi repository sozlesmesini tanimlar.</summary>
public interface INetworkRepository : IExtendedRepository<Network>
{
}
