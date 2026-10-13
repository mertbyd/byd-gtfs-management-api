using System;

namespace Byd.GtfsManagement.Dtos.Areas;

// islevi: areas.txt satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsAreaId dis GTFS kayit kimligidir.
/// <summary>GTFS bolge cevap alanlarini tasir.</summary>
public class AreaDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsAreaId { get; set; } = default!;
    public string? AreaName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
