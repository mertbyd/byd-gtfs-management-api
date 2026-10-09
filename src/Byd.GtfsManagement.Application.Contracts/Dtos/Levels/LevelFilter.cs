using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: Level liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Kat sorgusunun izinli alanlarini belirler.
/// <summary>GTFS kat filtrelerini tasir.</summary>
public class LevelFilter
{
    [DataTable(LevelFilterFields.FeedId, LevelFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(LevelFilterFields.GtfsLevelId, LevelFilterFields.PropertyPaths.GtfsLevelId)]
    public DataTableFilterItem? GtfsLevelId { get; set; }

    [DataTable(LevelFilterFields.LevelIndex, LevelFilterFields.PropertyPaths.LevelIndex)]
    public DataTableFilterItem? LevelIndex { get; set; }

    [DataTable(LevelFilterFields.Name, LevelFilterFields.PropertyPaths.Name)]
    public DataTableFilterItem? Name { get; set; }

    [DataTable(LevelFilterFields.CreationTime, LevelFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
