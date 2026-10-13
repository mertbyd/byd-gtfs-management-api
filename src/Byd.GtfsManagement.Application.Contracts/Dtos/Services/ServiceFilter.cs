using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Services;

// islevi: Service liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class ServiceFilter
{
    [DataTable(ServiceFilterFields.FeedId, ServiceFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(ServiceFilterFields.GtfsServiceId, ServiceFilterFields.PropertyPaths.GtfsServiceId)]
    public DataTableFilterItem? GtfsServiceId { get; set; }

    [DataTable(ServiceFilterFields.CreationTime, ServiceFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
