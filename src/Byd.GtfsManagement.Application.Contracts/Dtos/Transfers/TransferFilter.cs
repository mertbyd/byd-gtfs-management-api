using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Transfers;

// islevi: Transfer liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Aktarma sorgusunun izinli alanlarini belirler.
/// <summary>GTFS aktarma filtrelerini tasir.</summary>
public class TransferFilter : IFeedFilter
{
    [DataTable(TransferFilterFields.FeedId, TransferFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(TransferFilterFields.FromStopId, TransferFilterFields.PropertyPaths.FromStopId)]
    public DataTableFilterItem? FromStopId { get; set; }

    [DataTable(TransferFilterFields.ToStopId, TransferFilterFields.PropertyPaths.ToStopId)]
    public DataTableFilterItem? ToStopId { get; set; }

    [DataTable(TransferFilterFields.FromRouteId, TransferFilterFields.PropertyPaths.FromRouteId)]
    public DataTableFilterItem? FromRouteId { get; set; }

    [DataTable(TransferFilterFields.ToRouteId, TransferFilterFields.PropertyPaths.ToRouteId)]
    public DataTableFilterItem? ToRouteId { get; set; }

    [DataTable(TransferFilterFields.FromTripId, TransferFilterFields.PropertyPaths.FromTripId)]
    public DataTableFilterItem? FromTripId { get; set; }

    [DataTable(TransferFilterFields.ToTripId, TransferFilterFields.PropertyPaths.ToTripId)]
    public DataTableFilterItem? ToTripId { get; set; }

    [DataTable(TransferFilterFields.TransferType, TransferFilterFields.PropertyPaths.TransferType)]
    public DataTableFilterItem? TransferType { get; set; }

    [DataTable(TransferFilterFields.CreationTime, TransferFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
