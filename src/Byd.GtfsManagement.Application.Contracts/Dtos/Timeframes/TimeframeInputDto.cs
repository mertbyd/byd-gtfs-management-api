namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: Timeframe duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS zaman araligi ortak istek alanlarini tasir.</summary>
public abstract class TimeframeInputDto
{
    public string TimeframeGroupId { get; set; } = default!;
    public string GtfsServiceId { get; set; } = default!;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}
