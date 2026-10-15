using System;

namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: FareProduct satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret urunu cevap alanlarini tasir.</summary>
public class FareProductDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string GtfsFareProductId { get; set; } = default!;
    public string? GtfsRiderCategoryId { get; set; }
    public string? GtfsFareMediaId { get; set; }
    public string? FareProductName { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = default!;
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
