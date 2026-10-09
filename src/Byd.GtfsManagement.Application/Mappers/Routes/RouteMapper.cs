using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Models.Routes;
using Byd.GtfsManagement.Mappers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Routes;

// islevi: Route domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class RouteMapper
    : IGtfsCrudMapper<Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteCreateModel, RouteUpdateModel>
{
    public partial RouteCreateModel Map(CreateRouteDto input);
    public partial RouteUpdateModel Map(UpdateRouteDto input);
    public partial RouteDto Map(Route entity);
}
