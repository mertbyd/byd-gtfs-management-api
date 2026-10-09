using System;

namespace Byd.GtfsManagement.Models.Pathways;

// islevi: Pathway ekleme ve guncelleme icin ortak alanlari tasir.
// sistemdeki gorevi: Yuruyus yolu girdilerini Domain katmanina tasir.
/// <summary>GTFS yuruyus yolu ortak girdi modelini tasir.</summary>
public class PathwayInputModel
{
    public string GtfsPathwayId { get; set; } = default!;
    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
    public int PathwayMode { get; set; }
    public int IsBidirectional { get; set; }
    public double? Length { get; set; }
    public int? TraversalTime { get; set; }
    public int? StairCount { get; set; }
    public double? MaxSlope { get; set; }
    public double? MinWidth { get; set; }
    public string? SignpostedAs { get; set; }
    public string? ReversedSignpostedAs { get; set; }
}
