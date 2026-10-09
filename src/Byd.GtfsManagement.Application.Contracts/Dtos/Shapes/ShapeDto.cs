using System;

namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: shapes.txt satirini ve geometri alanlarini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsShapeId dis kimliktir.
/// <summary>GTFS sekil cevap alanlarini tasir.</summary>
public class ShapeDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsShapeId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Sequence { get; set; }
    public double? DistTraveled { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
