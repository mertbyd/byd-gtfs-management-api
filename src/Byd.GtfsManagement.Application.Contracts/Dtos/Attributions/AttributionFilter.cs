using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Attributions;

// islevi: Attribution liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Atif sorgusunun izinli alanlarini belirler.
/// <summary>GTFS atif filtrelerini tasir.</summary>
public class AttributionFilter
{
    [DataTable(AttributionFilterFields.FeedId, AttributionFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(AttributionFilterFields.AgencyId, AttributionFilterFields.PropertyPaths.AgencyId)]
    public DataTableFilterItem? AgencyId { get; set; }

    [DataTable(AttributionFilterFields.RouteId, AttributionFilterFields.PropertyPaths.RouteId)]
    public DataTableFilterItem? RouteId { get; set; }

    [DataTable(AttributionFilterFields.TripId, AttributionFilterFields.PropertyPaths.TripId)]
    public DataTableFilterItem? TripId { get; set; }

    [DataTable(AttributionFilterFields.GtfsAttributionId, AttributionFilterFields.PropertyPaths.GtfsAttributionId)]
    public DataTableFilterItem? GtfsAttributionId { get; set; }

    [DataTable(AttributionFilterFields.OrganizationName, AttributionFilterFields.PropertyPaths.OrganizationName)]
    public DataTableFilterItem? OrganizationName { get; set; }

    [DataTable(AttributionFilterFields.CreationTime, AttributionFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
