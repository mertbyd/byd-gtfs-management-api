namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: Shape ekleme ve guncelleme istekleri icin ortak GTFS alanlarini tasir.
// sistemdeki gorevi: HTTP girdi sozlesmesini tanimlar.
/// <summary>GTFS sekil ortak girdi DTO alanlarini tasir.</summary>
public class ShapeInputDto
{
    public string GtfsShapeId { get; set; } = default!;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Sequence { get; set; }
    public double? DistTraveled { get; set; }
}
