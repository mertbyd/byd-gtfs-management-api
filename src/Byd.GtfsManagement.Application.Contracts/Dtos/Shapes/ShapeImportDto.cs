using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: shapes.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Shape ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Shapes, GtfsImportOrders.Shape)]
public class ShapeImportDto
{
    [Name(GtfsColumnNames.ShapeId)]
    public string GtfsShapeId { get; set; } = default!;

    [Name(GtfsColumnNames.ShapePtLat)]
    public double Latitude { get; set; }

    [Name(GtfsColumnNames.ShapePtLon)]
    public double Longitude { get; set; }

    [Name(GtfsColumnNames.ShapePtSequence)]
    public int Sequence { get; set; }

    [Name(GtfsColumnNames.ShapeDistTraveled)]
    public double? DistTraveled { get; set; }
}
