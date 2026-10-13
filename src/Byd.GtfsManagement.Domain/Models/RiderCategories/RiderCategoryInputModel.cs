namespace Byd.GtfsManagement.Models.RiderCategories;

// islevi: RiderCategory olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS yolcu kategorisi ortak domain girdi modelini tasir.</summary>
public abstract class RiderCategoryInputModel
{
    public string GtfsRiderCategoryId { get; set; } = default!;
    public string RiderCategoryName { get; set; } = default!;
    public int? IsDefaultFareCategory { get; set; }
    public string? EligibilityUrl { get; set; }
}
