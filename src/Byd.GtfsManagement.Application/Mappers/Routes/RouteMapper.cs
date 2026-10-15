using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Models.Routes;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Routes;

// islevi: Route domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class RouteMapper
    : IGtfsArchiveMapper<RouteImportDto, Route>, IGtfsCrudMapper<Route, CreateRouteDto, UpdateRouteDto, RouteDto, RouteCreateModel, RouteUpdateModel>
{
    public partial RouteCreateModel Map(CreateRouteDto input);
    public partial RouteUpdateModel Map(UpdateRouteDto input);
    public partial RouteDto Map(Route entity);
    public partial RouteDetailDto Map(RouteDetailModel model);

    // CSV satirini entity'ye esler.
    public partial Route Map(RouteImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial RouteImportDto MapToImport(Route entity);
}
