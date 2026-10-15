using System;

namespace Byd.GtfsManagement.Dtos.Areas;

// islevi: areas.txt satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsAreaId dis GTFS kayit kimligidir.
/// <summary>GTFS bolge cevap alanlarini tasir.</summary>
public class AreaDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsAreaId { get; set; } = default!;
    public string? AreaName { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
