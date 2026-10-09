namespace Byd.GtfsManagement.Dtos.Areas;

// islevi: areas.txt duzenlenebilir bolge alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS bolge ortak istek alanlarini tasir.</summary>
public abstract class AreaInputDto
{
    public string GtfsAreaId { get; set; } = default!;
    public string? AreaName { get; set; }
}
