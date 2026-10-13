namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: LocationGroup duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS konum grubu ortak istek alanlarini tasir.</summary>
public abstract class LocationGroupInputDto
{
    public string GtfsLocationGroupId { get; set; } = default!;
    public string? LocationGroupName { get; set; }
}
