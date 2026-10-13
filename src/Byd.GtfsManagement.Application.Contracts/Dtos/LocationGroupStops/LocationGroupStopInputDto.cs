namespace Byd.GtfsManagement.Dtos.LocationGroupStops;

// islevi: LocationGroupStop duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS konum grubu durak iliskisi ortak istek alanlarini tasir.</summary>
public abstract class LocationGroupStopInputDto
{
    public string GtfsLocationGroupId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
}
