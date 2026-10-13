using System;

namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: FareProduct satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret urunu cevap alanlarini tasir.</summary>
public class FareProductDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string GtfsFareProductId { get; set; } = default!;
    public string? GtfsRiderCategoryId { get; set; }
    public string? GtfsFareMediaId { get; set; }
    public string? FareProductName { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
