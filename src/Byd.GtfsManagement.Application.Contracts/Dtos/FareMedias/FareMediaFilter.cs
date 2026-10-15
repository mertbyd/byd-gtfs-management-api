using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareMedias;

// islevi: FareMedia liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ucret medyasi filtrelerini tasir.</summary>
public class FareMediaFilter : IFeedFilter
{
    [DataTable(FareMediaFilterFields.FeedId, FareMediaFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareMediaFilterFields.GtfsFareMediaId, FareMediaFilterFields.PropertyPaths.GtfsFareMediaId)]
    public DataTableFilterItem? GtfsFareMediaId { get; set; }

    [DataTable(FareMediaFilterFields.FareMediaName, FareMediaFilterFields.PropertyPaths.FareMediaName)]
    public DataTableFilterItem? FareMediaName { get; set; }

    [DataTable(FareMediaFilterFields.FareMediaType, FareMediaFilterFields.PropertyPaths.FareMediaType)]
    public DataTableFilterItem? FareMediaType { get; set; }

    [DataTable(FareMediaFilterFields.CreationTime, FareMediaFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
