using System;

namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: Timeframe satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS zaman araligi cevap alanlarini tasir.</summary>
public class TimeframeDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string TimeframeGroupId { get; set; } = default!;
    public string GtfsServiceId { get; set; } = default!;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
