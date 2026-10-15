using System;

namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: levels.txt satirini ve kat alanlarini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsLevelId dis kimliktir.
/// <summary>GTFS kat cevap alanlarini tasir.</summary>
public class LevelDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsLevelId { get; set; } = default!;
    public double LevelIndex { get; set; }
    public string? Name { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
