using System;

namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: StopArea satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS durak bolge iliskisi cevap alanlarini tasir.</summary>
public class StopAreaDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsAreaId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
