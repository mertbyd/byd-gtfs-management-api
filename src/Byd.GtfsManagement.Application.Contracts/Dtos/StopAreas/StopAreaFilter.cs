using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: StopArea liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS durak bolge iliskisi filtrelerini tasir.</summary>
public class StopAreaFilter : IFeedFilter
{
    [DataTable(StopAreaFilterFields.FeedId, StopAreaFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(StopAreaFilterFields.GtfsAreaId, StopAreaFilterFields.PropertyPaths.GtfsAreaId)]
    public DataTableFilterItem? GtfsAreaId { get; set; }

    [DataTable(StopAreaFilterFields.GtfsStopId, StopAreaFilterFields.PropertyPaths.GtfsStopId)]
    public DataTableFilterItem? GtfsStopId { get; set; }

    [DataTable(StopAreaFilterFields.CreationTime, StopAreaFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
