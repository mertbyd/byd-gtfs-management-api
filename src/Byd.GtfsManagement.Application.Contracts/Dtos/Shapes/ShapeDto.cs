using System;

namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: shapes.txt satirini ve geometri alanlarini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; GtfsShapeId dis kimliktir.
/// <summary>GTFS sekil cevap alanlarini tasir.</summary>
public class ShapeDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsShapeId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Sequence { get; set; }
    public double? DistTraveled { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
