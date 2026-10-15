using System;

namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: Timeframe satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS zaman araligi cevap alanlarini tasir.</summary>
public class TimeframeDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public string TimeframeGroupId { get; set; } = default!;

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsServiceId { get; set; } = default!;
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
