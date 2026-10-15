using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.RiderCategories;

// islevi: RiderCategory liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS yolcu kategorisi filtrelerini tasir.</summary>
public class RiderCategoryFilter : IFeedFilter
{
    [DataTable(RiderCategoryFilterFields.FeedId, RiderCategoryFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(RiderCategoryFilterFields.GtfsRiderCategoryId, RiderCategoryFilterFields.PropertyPaths.GtfsRiderCategoryId)]
    public DataTableFilterItem? GtfsRiderCategoryId { get; set; }

    [DataTable(RiderCategoryFilterFields.RiderCategoryName, RiderCategoryFilterFields.PropertyPaths.RiderCategoryName)]
    public DataTableFilterItem? RiderCategoryName { get; set; }

    [DataTable(RiderCategoryFilterFields.IsDefaultFareCategory, RiderCategoryFilterFields.PropertyPaths.IsDefaultFareCategory)]
    public DataTableFilterItem? IsDefaultFareCategory { get; set; }

    [DataTable(RiderCategoryFilterFields.EligibilityUrl, RiderCategoryFilterFields.PropertyPaths.EligibilityUrl)]
    public DataTableFilterItem? EligibilityUrl { get; set; }

    [DataTable(RiderCategoryFilterFields.CreationTime, RiderCategoryFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
