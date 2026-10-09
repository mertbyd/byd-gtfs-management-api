namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: RouteNetwork duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS hat ag iliskisi ortak istek alanlarini tasir.</summary>
public abstract class RouteNetworkInputDto
{
    public string GtfsNetworkId { get; set; } = default!;
    public string GtfsRouteId { get; set; } = default!;
}
