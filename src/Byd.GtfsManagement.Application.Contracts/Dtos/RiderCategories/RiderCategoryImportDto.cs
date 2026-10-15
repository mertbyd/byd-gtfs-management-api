using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.RiderCategories;

// islevi: rider_categories.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: RiderCategory ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.RiderCategories, GtfsImportOrders.RiderCategory)]
public class RiderCategoryImportDto
{
    [Name(GtfsColumnNames.RiderCategoryId)]
    public string GtfsRiderCategoryId { get; set; } = default!;

    [Name(GtfsColumnNames.RiderCategoryName)]
    public string RiderCategoryName { get; set; } = default!;

    [Name(GtfsColumnNames.IsDefaultFareCategory)]
    public int? IsDefaultFareCategory { get; set; }

    [Name(GtfsColumnNames.EligibilityUrl)]
    public string? EligibilityUrl { get; set; }
}
