using System;

namespace Byd.GtfsManagement.Dtos.RiderCategories;

// islevi: RiderCategory satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS yolcu kategorisi cevap alanlarini tasir.</summary>
public class RiderCategoryDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsRiderCategoryId { get; set; } = default!;
    public string RiderCategoryName { get; set; } = default!;
    public int? IsDefaultFareCategory { get; set; }
    public string? EligibilityUrl { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
