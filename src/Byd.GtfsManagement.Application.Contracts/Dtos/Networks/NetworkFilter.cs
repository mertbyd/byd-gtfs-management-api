using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: Network liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ulasim agi filtrelerini tasir.</summary>
public class NetworkFilter
{
    [DataTable(NetworkFilterFields.FeedId, NetworkFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(NetworkFilterFields.GtfsNetworkId, NetworkFilterFields.PropertyPaths.GtfsNetworkId)]
    public DataTableFilterItem? GtfsNetworkId { get; set; }

    [DataTable(NetworkFilterFields.NetworkName, NetworkFilterFields.PropertyPaths.NetworkName)]
    public DataTableFilterItem? NetworkName { get; set; }

    [DataTable(NetworkFilterFields.CreationTime, NetworkFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
