using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: RouteNetwork liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS hat ag iliskisi filtrelerini tasir.</summary>
public class RouteNetworkFilter
{
    [DataTable(RouteNetworkFilterFields.FeedId, RouteNetworkFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(RouteNetworkFilterFields.GtfsNetworkId, RouteNetworkFilterFields.PropertyPaths.GtfsNetworkId)]
    public DataTableFilterItem? GtfsNetworkId { get; set; }

    [DataTable(RouteNetworkFilterFields.GtfsRouteId, RouteNetworkFilterFields.PropertyPaths.GtfsRouteId)]
    public DataTableFilterItem? GtfsRouteId { get; set; }

    [DataTable(RouteNetworkFilterFields.CreationTime, RouteNetworkFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
