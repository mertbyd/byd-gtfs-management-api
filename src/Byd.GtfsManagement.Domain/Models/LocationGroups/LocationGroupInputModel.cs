namespace Byd.GtfsManagement.Models.LocationGroups;

// islevi: LocationGroup olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS konum grubu ortak domain girdi modelini tasir.</summary>
public abstract class LocationGroupInputModel
{
    public string GtfsLocationGroupId { get; set; } = default!;
    public string? LocationGroupName { get; set; }
}
