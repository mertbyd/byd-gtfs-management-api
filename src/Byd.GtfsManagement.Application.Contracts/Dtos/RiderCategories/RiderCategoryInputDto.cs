namespace Byd.GtfsManagement.Dtos.RiderCategories;

// islevi: RiderCategory duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS yolcu kategorisi ortak istek alanlarini tasir.</summary>
public abstract class RiderCategoryInputDto
{
    public string GtfsRiderCategoryId { get; set; } = default!;
    public string RiderCategoryName { get; set; } = default!;
    public int? IsDefaultFareCategory { get; set; }
    public string? EligibilityUrl { get; set; }
}
