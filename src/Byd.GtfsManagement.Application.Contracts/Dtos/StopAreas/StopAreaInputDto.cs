namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: StopArea duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS durak bolge iliskisi ortak istek alanlarini tasir.</summary>
public abstract class StopAreaInputDto
{
    public string GtfsAreaId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
}
