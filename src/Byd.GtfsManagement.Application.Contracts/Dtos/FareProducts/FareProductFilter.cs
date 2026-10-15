using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: FareProduct liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ucret urunu filtrelerini tasir.</summary>
public class FareProductFilter : IFeedFilter
{
    [DataTable(FareProductFilterFields.FeedId, FareProductFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareProductFilterFields.GtfsFareProductId, FareProductFilterFields.PropertyPaths.GtfsFareProductId)]
    public DataTableFilterItem? GtfsFareProductId { get; set; }

    [DataTable(FareProductFilterFields.FareProductName, FareProductFilterFields.PropertyPaths.FareProductName)]
    public DataTableFilterItem? FareProductName { get; set; }

    [DataTable(FareProductFilterFields.GtfsRiderCategoryId, FareProductFilterFields.PropertyPaths.GtfsRiderCategoryId)]
    public DataTableFilterItem? GtfsRiderCategoryId { get; set; }

    [DataTable(FareProductFilterFields.GtfsFareMediaId, FareProductFilterFields.PropertyPaths.GtfsFareMediaId)]
    public DataTableFilterItem? GtfsFareMediaId { get; set; }

    [DataTable(FareProductFilterFields.Amount, FareProductFilterFields.PropertyPaths.Amount)]
    public DataTableFilterItem? Amount { get; set; }

    [DataTable(FareProductFilterFields.Currency, FareProductFilterFields.PropertyPaths.Currency)]
    public DataTableFilterItem? Currency { get; set; }

    [DataTable(FareProductFilterFields.CreationTime, FareProductFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
