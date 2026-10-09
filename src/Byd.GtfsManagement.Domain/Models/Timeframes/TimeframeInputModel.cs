namespace Byd.GtfsManagement.Models.Timeframes;

// islevi: Timeframe olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS zaman araligi ortak domain girdi modelini tasir.</summary>
public abstract class TimeframeInputModel
{
    public string TimeframeGroupId { get; set; } = default!;
    public string GtfsServiceId { get; set; } = default!;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
}
