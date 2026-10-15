using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: Timeframe liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS zaman araligi filtrelerini tasir.</summary>
public class TimeframeFilter : IFeedFilter
{
    [DataTable(TimeframeFilterFields.FeedId, TimeframeFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(TimeframeFilterFields.TimeframeGroupId, TimeframeFilterFields.PropertyPaths.TimeframeGroupId)]
    public DataTableFilterItem? TimeframeGroupId { get; set; }

    [DataTable(TimeframeFilterFields.StartTime, TimeframeFilterFields.PropertyPaths.StartTime)]
    public DataTableFilterItem? StartTime { get; set; }

    [DataTable(TimeframeFilterFields.EndTime, TimeframeFilterFields.PropertyPaths.EndTime)]
    public DataTableFilterItem? EndTime { get; set; }

    [DataTable(TimeframeFilterFields.GtfsServiceId, TimeframeFilterFields.PropertyPaths.GtfsServiceId)]
    public DataTableFilterItem? GtfsServiceId { get; set; }

    [DataTable(TimeframeFilterFields.CreationTime, TimeframeFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
