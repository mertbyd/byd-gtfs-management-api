using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: Route liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class RouteFilter : IFeedFilter
{
    [DataTable(RouteFilterFields.FeedId, RouteFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(RouteFilterFields.AgencyId, RouteFilterFields.PropertyPaths.AgencyId)]
    public DataTableFilterItem? AgencyId { get; set; }

    [DataTable(RouteFilterFields.RouteTypeId, RouteFilterFields.PropertyPaths.RouteTypeId)]
    public DataTableFilterItem? RouteTypeId { get; set; }

    [DataTable(RouteFilterFields.GtfsRouteId, RouteFilterFields.PropertyPaths.GtfsRouteId)]
    public DataTableFilterItem? GtfsRouteId { get; set; }

    [DataTable(RouteFilterFields.ShortName, RouteFilterFields.PropertyPaths.ShortName)]
    public DataTableFilterItem? ShortName { get; set; }

    [DataTable(RouteFilterFields.LongName, RouteFilterFields.PropertyPaths.LongName)]
    public DataTableFilterItem? LongName { get; set; }

    [DataTable(RouteFilterFields.SortOrder, RouteFilterFields.PropertyPaths.SortOrder)]
    public DataTableFilterItem? SortOrder { get; set; }

    [DataTable(RouteFilterFields.CreationTime, RouteFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
