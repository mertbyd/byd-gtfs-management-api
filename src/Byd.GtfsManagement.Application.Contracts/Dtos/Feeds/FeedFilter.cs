using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Feed liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class FeedFilter
{
    [DataTable(FeedFilterFields.FeedId, FeedFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FeedFilterFields.Name, FeedFilterFields.PropertyPaths.Name)]
    public DataTableFilterItem? Name { get; set; }

    [DataTable(FeedFilterFields.IsActive, FeedFilterFields.PropertyPaths.IsActive)]
    public DataTableFilterItem? IsActive { get; set; }

    [DataTable(FeedFilterFields.Status, FeedFilterFields.PropertyPaths.Status)]
    public DataTableFilterItem? Status { get; set; }

    [DataTable(FeedFilterFields.CreationTime, FeedFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
