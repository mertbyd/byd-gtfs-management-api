using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.StopTimes;

// islevi: StopTime filtre alanlarini tipli DataTable motoruna tanitir.
// sistemdeki gorevi: Liste sorgusunun alan izin listesidir.
/// <summary>StopTime liste filtrelerini tasir.</summary>
public class StopTimeFilter : IFeedFilter
{
    [DataTable(StopTimeFilterFields.FeedId, StopTimeFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(StopTimeFilterFields.TripId, StopTimeFilterFields.PropertyPaths.TripId)]
    public DataTableFilterItem? TripId { get; set; }

    [DataTable(StopTimeFilterFields.StopId, StopTimeFilterFields.PropertyPaths.StopId)]
    public DataTableFilterItem? StopId { get; set; }

    [DataTable(StopTimeFilterFields.StopSequence, StopTimeFilterFields.PropertyPaths.StopSequence)]
    public DataTableFilterItem? StopSequence { get; set; }

    [DataTable(StopTimeFilterFields.ArrivalTime, StopTimeFilterFields.PropertyPaths.ArrivalTime)]
    public DataTableFilterItem? ArrivalTime { get; set; }

    [DataTable(StopTimeFilterFields.DepartureTime, StopTimeFilterFields.PropertyPaths.DepartureTime)]
    public DataTableFilterItem? DepartureTime { get; set; }

    [DataTable(StopTimeFilterFields.CreationTime, StopTimeFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
