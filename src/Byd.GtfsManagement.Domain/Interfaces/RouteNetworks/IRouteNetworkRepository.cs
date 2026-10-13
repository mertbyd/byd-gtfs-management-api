using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.RouteNetworks;

namespace Byd.GtfsManagement.Interfaces.RouteNetworks;

// islevi: RouteNetwork entity'si icin genisletilmis veri erisim sozlesmesini bildirir.
// sistemdeki gorevi: Hat ag iliskisi sorgularini ve detay yuklemelerini soyutlar.
/// <summary>GTFS hat ag iliskisi repository sozlesmesini tanimlar.</summary>
public interface IRouteNetworkRepository : IExtendedRepository<RouteNetwork>
{
}
