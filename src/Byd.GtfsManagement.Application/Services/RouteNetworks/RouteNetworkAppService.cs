using Byd.GtfsManagement.Dtos.RouteNetworks;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Byd.GtfsManagement.Interfaces.RouteNetworks;
using Byd.GtfsManagement.Managers.RouteNetworks;
using Byd.GtfsManagement.Mappers.RouteNetworks;
using Byd.GtfsManagement.Models.RouteNetworks;

namespace Byd.GtfsManagement.Services.RouteNetworks;

// islevi: RouteNetwork CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS hat ag iliskisi CRUD servisini ortak tabana baglar.</summary>
public class RouteNetworkAppService
    : GtfsImportExportAppServiceBase<
        RouteNetwork, CreateRouteNetworkDto, UpdateRouteNetworkDto, RouteNetworkDto, RouteNetworkFilter,
        RouteNetworkCreateModel, RouteNetworkUpdateModel, RouteNetworkManager, IRouteNetworkRepository, RouteNetworkMapper, RouteNetworkImportDto>,
        IRouteNetworkAppService
{
}
