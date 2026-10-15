using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: fare_products.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareProduct ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareProducts, GtfsImportOrders.FareProduct)]
public class FareProductImportDto
{
    [Name(GtfsColumnNames.FareProductId)]
    public string GtfsFareProductId { get; set; } = default!;

    [Name(GtfsColumnNames.RiderCategoryId)]
    public string? GtfsRiderCategoryId { get; set; }

    [Name(GtfsColumnNames.FareMediaId)]
    public string? GtfsFareMediaId { get; set; }

    [Name(GtfsColumnNames.FareProductName)]
    public string? FareProductName { get; set; }

    [Name(GtfsColumnNames.Amount)]
    public decimal Amount { get; set; }

    [Name(GtfsColumnNames.Currency)]
    public string Currency { get; set; } = default!;
}
