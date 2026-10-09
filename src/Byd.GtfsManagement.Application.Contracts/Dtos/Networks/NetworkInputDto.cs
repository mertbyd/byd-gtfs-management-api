namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: Network duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ulasim agi ortak istek alanlarini tasir.</summary>
public abstract class NetworkInputDto
{
    public string GtfsNetworkId { get; set; } = default!;
    public string? NetworkName { get; set; }
}
