using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Pathways;

// islevi: Pathway liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Yuruyus yolu sorgusunun izinli alanlarini belirler.
/// <summary>GTFS yuruyus yolu filtrelerini tasir.</summary>
public class PathwayFilter : IFeedFilter
{
    [DataTable(PathwayFilterFields.FeedId, PathwayFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(PathwayFilterFields.GtfsPathwayId, PathwayFilterFields.PropertyPaths.GtfsPathwayId)]
    public DataTableFilterItem? GtfsPathwayId { get; set; }

    [DataTable(PathwayFilterFields.FromStopId, PathwayFilterFields.PropertyPaths.FromStopId)]
    public DataTableFilterItem? FromStopId { get; set; }

    [DataTable(PathwayFilterFields.ToStopId, PathwayFilterFields.PropertyPaths.ToStopId)]
    public DataTableFilterItem? ToStopId { get; set; }

    [DataTable(PathwayFilterFields.PathwayMode, PathwayFilterFields.PropertyPaths.PathwayMode)]
    public DataTableFilterItem? PathwayMode { get; set; }

    [DataTable(PathwayFilterFields.IsBidirectional, PathwayFilterFields.PropertyPaths.IsBidirectional)]
    public DataTableFilterItem? IsBidirectional { get; set; }

    [DataTable(PathwayFilterFields.Length, PathwayFilterFields.PropertyPaths.Length)]
    public DataTableFilterItem? Length { get; set; }

    [DataTable(PathwayFilterFields.TraversalTime, PathwayFilterFields.PropertyPaths.TraversalTime)]
    public DataTableFilterItem? TraversalTime { get; set; }

    [DataTable(PathwayFilterFields.CreationTime, PathwayFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
