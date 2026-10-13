using System;

namespace Byd.GtfsManagement.Dtos.Pathways;

// islevi: pathways.txt duzenlenebilir yuruyus yolu alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS yuruyus yolu ortak istek alanlarini tasir.</summary>
public abstract class PathwayInputDto
{
    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
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
}
