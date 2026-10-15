using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.LocationGroupStops;

// islevi: LocationGroupStop liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS konum grubu durak iliskisi filtrelerini tasir.</summary>
public class LocationGroupStopFilter : IFeedFilter
{
    [DataTable(LocationGroupStopFilterFields.FeedId, LocationGroupStopFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(LocationGroupStopFilterFields.GtfsLocationGroupId, LocationGroupStopFilterFields.PropertyPaths.GtfsLocationGroupId)]
    public DataTableFilterItem? GtfsLocationGroupId { get; set; }

    [DataTable(LocationGroupStopFilterFields.GtfsStopId, LocationGroupStopFilterFields.PropertyPaths.GtfsStopId)]
    public DataTableFilterItem? GtfsStopId { get; set; }

    [DataTable(LocationGroupStopFilterFields.CreationTime, LocationGroupStopFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
