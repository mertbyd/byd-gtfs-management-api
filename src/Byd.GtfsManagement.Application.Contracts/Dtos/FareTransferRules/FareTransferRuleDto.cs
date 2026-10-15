using System;

namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: FareTransferRule satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret aktarma kurali cevap alanlarini tasir.</summary>
public class FareTransferRuleDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public string? FromLegGroupId { get; set; }
    public string? ToLegGroupId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string? GtfsFareProductId { get; set; }
    public int? TransferCount { get; set; }
    public int? DurationLimit { get; set; }
    public int? DurationLimitType { get; set; }
    public int FareTransferType { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
