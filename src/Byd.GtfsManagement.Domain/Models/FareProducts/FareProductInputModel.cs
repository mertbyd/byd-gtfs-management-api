namespace Byd.GtfsManagement.Models.FareProducts;

// islevi: FareProduct olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret urunu ortak domain girdi modelini tasir.</summary>
public abstract class FareProductInputModel
{
    public string GtfsFareProductId { get; set; } = default!;
    public string? GtfsRiderCategoryId { get; set; }
    public string? GtfsFareMediaId { get; set; }
    public string? FareProductName { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = default!;
}
