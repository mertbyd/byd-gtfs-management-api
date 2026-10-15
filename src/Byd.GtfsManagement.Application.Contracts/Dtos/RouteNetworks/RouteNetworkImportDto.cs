using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: route_networks.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: RouteNetwork ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.RouteNetworks, GtfsImportOrders.RouteNetwork)]
public class RouteNetworkImportDto
{
    [Name(GtfsColumnNames.NetworkId)]
    public string GtfsNetworkId { get; set; } = default!;

    [Name(GtfsColumnNames.RouteId)]
    public string GtfsRouteId { get; set; } = default!;
}
