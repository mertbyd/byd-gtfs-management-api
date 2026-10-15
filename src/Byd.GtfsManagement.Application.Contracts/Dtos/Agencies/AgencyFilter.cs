using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Agencies;

// islevi: Agency liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class AgencyFilter : IFeedFilter
{
    [DataTable(AgencyFilterFields.FeedId, AgencyFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(AgencyFilterFields.CemvSupportId, AgencyFilterFields.PropertyPaths.CemvSupportId)]
    public DataTableFilterItem? CemvSupportId { get; set; }

    [DataTable(AgencyFilterFields.GtfsAgencyId, AgencyFilterFields.PropertyPaths.GtfsAgencyId)]
    public DataTableFilterItem? GtfsAgencyId { get; set; }

    [DataTable(AgencyFilterFields.Name, AgencyFilterFields.PropertyPaths.Name)]
    public DataTableFilterItem? Name { get; set; }

    [DataTable(AgencyFilterFields.Timezone, AgencyFilterFields.PropertyPaths.Timezone)]
    public DataTableFilterItem? Timezone { get; set; }

    [DataTable(AgencyFilterFields.Language, AgencyFilterFields.PropertyPaths.Language)]
    public DataTableFilterItem? Language { get; set; }

    [DataTable(AgencyFilterFields.CreationTime, AgencyFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
