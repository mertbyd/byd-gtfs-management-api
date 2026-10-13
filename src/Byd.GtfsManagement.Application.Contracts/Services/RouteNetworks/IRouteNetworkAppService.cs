using Byd.GtfsManagement.Dtos.RouteNetworks;

namespace Byd.GtfsManagement.Services.RouteNetworks;

// islevi: RouteNetwork CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS hat ag iliskisi CRUD API sozlesmesini bildirir.</summary>
public interface IRouteNetworkAppService
    : IGtfsCrudAppService<CreateRouteNetworkDto, UpdateRouteNetworkDto, RouteNetworkDto, RouteNetworkFilter>
{
}
