using System;

namespace Byd.GtfsManagement.Dtos.FareLegRules;

// islevi: FareLegRule satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret etap kurali cevap alanlarini tasir.</summary>
public class FareLegRuleDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string? LegGroupId { get; set; }
    public string? NetworkId { get; set; }
    public string? FromAreaId { get; set; }
    public string? ToAreaId { get; set; }
    public string? FromTimeframeGroupId { get; set; }
    public string? ToTimeframeGroupId { get; set; }
    public string GtfsFareProductId { get; set; } = default!;
    public int? RulePriority { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
