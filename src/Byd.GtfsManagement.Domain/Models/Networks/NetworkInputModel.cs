namespace Byd.GtfsManagement.Models.Networks;

// islevi: Network olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ulasim agi ortak domain girdi modelini tasir.</summary>
public abstract class NetworkInputModel
{
    public string GtfsNetworkId { get; set; } = default!;
    public string? NetworkName { get; set; }
}
