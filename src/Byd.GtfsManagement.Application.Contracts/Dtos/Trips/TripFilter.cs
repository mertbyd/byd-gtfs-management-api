using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: Trip liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class TripFilter : IFeedFilter
{
    [DataTable(TripFilterFields.FeedId, TripFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(TripFilterFields.RouteId, TripFilterFields.PropertyPaths.RouteId)]
    public DataTableFilterItem? RouteId { get; set; }

    [DataTable(TripFilterFields.ServiceId, TripFilterFields.PropertyPaths.ServiceId)]
    public DataTableFilterItem? ServiceId { get; set; }

    [DataTable(TripFilterFields.TripDirectionId, TripFilterFields.PropertyPaths.TripDirectionId)]
    public DataTableFilterItem? TripDirectionId { get; set; }

    [DataTable(TripFilterFields.GtfsTripId, TripFilterFields.PropertyPaths.GtfsTripId)]
    public DataTableFilterItem? GtfsTripId { get; set; }

    [DataTable(TripFilterFields.Headsign, TripFilterFields.PropertyPaths.Headsign)]
    public DataTableFilterItem? Headsign { get; set; }

    [DataTable(TripFilterFields.BlockId, TripFilterFields.PropertyPaths.BlockId)]
    public DataTableFilterItem? BlockId { get; set; }

    [DataTable(TripFilterFields.CreationTime, TripFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
