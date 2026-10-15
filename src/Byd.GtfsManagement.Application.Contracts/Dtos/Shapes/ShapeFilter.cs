using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Shapes;

// islevi: Shape liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sekil sorgusunun izinli alanlarini belirler.
/// <summary>GTFS sekil filtrelerini tasir.</summary>
public class ShapeFilter : IFeedFilter
{
    [DataTable(ShapeFilterFields.FeedId, ShapeFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(ShapeFilterFields.GtfsShapeId, ShapeFilterFields.PropertyPaths.GtfsShapeId)]
    public DataTableFilterItem? GtfsShapeId { get; set; }

    [DataTable(ShapeFilterFields.Sequence, ShapeFilterFields.PropertyPaths.Sequence)]
    public DataTableFilterItem? Sequence { get; set; }

    [DataTable(ShapeFilterFields.Latitude, ShapeFilterFields.PropertyPaths.Latitude)]
    public DataTableFilterItem? Latitude { get; set; }

    [DataTable(ShapeFilterFields.Longitude, ShapeFilterFields.PropertyPaths.Longitude)]
    public DataTableFilterItem? Longitude { get; set; }

    [DataTable(ShapeFilterFields.DistTraveled, ShapeFilterFields.PropertyPaths.DistTraveled)]
    public DataTableFilterItem? DistTraveled { get; set; }

    [DataTable(ShapeFilterFields.CreationTime, ShapeFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
