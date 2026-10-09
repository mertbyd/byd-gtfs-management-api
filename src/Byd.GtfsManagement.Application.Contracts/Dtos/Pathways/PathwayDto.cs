using System;

namespace Byd.GtfsManagement.Dtos.Pathways;

// islevi: pathways.txt satirini, bagli duraklarin adlari ve yol alanlariyla public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class PathwayDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid FromStopId { get; set; }
    public string? FromStopName { get; set; }
    public Guid ToStopId { get; set; }
    public string? ToStopName { get; set; }
    public string GtfsPathwayId { get; set; } = default!;
    public int PathwayMode { get; set; }
    public int IsBidirectional { get; set; }
    public double? Length { get; set; }
    public int? TraversalTime { get; set; }
    public int? StairCount { get; set; }
    public double? MaxSlope { get; set; }
    public double? MinWidth { get; set; }
    public string? SignpostedAs { get; set; }
    public string? ReversedSignpostedAs { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
