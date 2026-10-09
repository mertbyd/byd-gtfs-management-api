using System;

namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: FareTransferRule satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret aktarma kurali cevap alanlarini tasir.</summary>
public class FareTransferRuleDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string? FromLegGroupId { get; set; }
    public string? ToLegGroupId { get; set; }
    public string? GtfsFareProductId { get; set; }
    public int? TransferCount { get; set; }
    public int? DurationLimit { get; set; }
    public int? DurationLimitType { get; set; }
    public int FareTransferType { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
