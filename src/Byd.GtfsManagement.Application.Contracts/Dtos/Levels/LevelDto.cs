using System;

namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: levels.txt satirini ve kat alanlarini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsLevelId dis kimliktir.
/// <summary>GTFS kat cevap alanlarini tasir.</summary>
public class LevelDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsLevelId { get; set; } = default!;
    public double LevelIndex { get; set; }
    public string? Name { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
