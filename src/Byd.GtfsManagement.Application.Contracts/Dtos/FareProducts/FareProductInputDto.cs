namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: FareProduct duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret urunu ortak istek alanlarini tasir.</summary>
public abstract class FareProductInputDto
{
    public string GtfsFareProductId { get; set; } = default!;
    public string? GtfsRiderCategoryId { get; set; }
    public string? GtfsFareMediaId { get; set; }
    public string? FareProductName { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = default!;
}
