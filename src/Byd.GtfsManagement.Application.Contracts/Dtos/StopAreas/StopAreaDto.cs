using System;

namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: StopArea satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS durak bolge iliskisi cevap alanlarini tasir.</summary>
public class StopAreaDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsAreaId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
