using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: LocationGroup liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS konum grubu filtrelerini tasir.</summary>
public class LocationGroupFilter : IFeedFilter
{
    [DataTable(LocationGroupFilterFields.FeedId, LocationGroupFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(LocationGroupFilterFields.GtfsLocationGroupId, LocationGroupFilterFields.PropertyPaths.GtfsLocationGroupId)]
    public DataTableFilterItem? GtfsLocationGroupId { get; set; }

    [DataTable(LocationGroupFilterFields.LocationGroupName, LocationGroupFilterFields.PropertyPaths.LocationGroupName)]
    public DataTableFilterItem? LocationGroupName { get; set; }

    [DataTable(LocationGroupFilterFields.CreationTime, LocationGroupFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
