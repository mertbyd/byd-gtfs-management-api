namespace Byd.GtfsManagement.Models.LocationGroupStops;

// islevi: LocationGroupStop olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS konum grubu durak iliskisi ortak domain girdi modelini tasir.</summary>
public abstract class LocationGroupStopInputModel
{
    public string GtfsLocationGroupId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
}
