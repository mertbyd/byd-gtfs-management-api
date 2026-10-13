namespace Byd.GtfsManagement.Models.RouteNetworks;

// islevi: RouteNetwork olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS hat ag iliskisi ortak domain girdi modelini tasir.</summary>
public abstract class RouteNetworkInputModel
{
    public string GtfsNetworkId { get; set; } = default!;
    public string GtfsRouteId { get; set; } = default!;
}
