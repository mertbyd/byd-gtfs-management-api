using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Areas;

// islevi: Area liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Bolge sorgusunun izinli alanlarini belirler.
/// <summary>GTFS bolge filtrelerini tasir.</summary>
public class AreaFilter
{
    [DataTable(AreaFilterFields.FeedId, AreaFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(AreaFilterFields.GtfsAreaId, AreaFilterFields.PropertyPaths.GtfsAreaId)]
    public DataTableFilterItem? GtfsAreaId { get; set; }

    [DataTable(AreaFilterFields.AreaName, AreaFilterFields.PropertyPaths.AreaName)]
    public DataTableFilterItem? AreaName { get; set; }

    [DataTable(AreaFilterFields.CreationTime, AreaFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
