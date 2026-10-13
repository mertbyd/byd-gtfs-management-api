using Byd.GtfsManagement.Dtos.RouteNetworks;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Byd.GtfsManagement.Models.RouteNetworks;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.RouteNetworks;

// islevi: RouteNetwork DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS hat ag iliskisi DTO ve modellerini esler.</summary>
[Mapper]
public partial class RouteNetworkMapper
    : IGtfsCrudMapper<RouteNetwork, CreateRouteNetworkDto, UpdateRouteNetworkDto, RouteNetworkDto,
        RouteNetworkCreateModel, RouteNetworkUpdateModel>
{
    public partial RouteNetworkCreateModel Map(CreateRouteNetworkDto input);
    public partial RouteNetworkUpdateModel Map(UpdateRouteNetworkDto input);
    public partial RouteNetworkDto Map(RouteNetwork entity);
}
