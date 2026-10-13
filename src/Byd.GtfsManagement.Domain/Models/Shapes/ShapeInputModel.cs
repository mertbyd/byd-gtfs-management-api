namespace Byd.GtfsManagement.Models.Shapes;

// islevi: Shape olusturma ve guncelleme icin ortak alanlari tasir.
// sistemdeki gorevi: Sekil girdilerini Domain katmanina tasir.
/// <summary>GTFS sekil ortak girdi modelini tasir.</summary>
public class ShapeInputModel
{
    public string GtfsShapeId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Sequence { get; set; }
    public double? DistTraveled { get; set; }
}
