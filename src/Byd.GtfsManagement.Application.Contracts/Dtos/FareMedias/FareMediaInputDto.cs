namespace Byd.GtfsManagement.Dtos.FareMedias;

// islevi: FareMedia duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret medyasi ortak istek alanlarini tasir.</summary>
public abstract class FareMediaInputDto
{
    public string GtfsFareMediaId { get; set; } = default!;
    public string? FareMediaName { get; set; }
    public int FareMediaType { get; set; }
}
