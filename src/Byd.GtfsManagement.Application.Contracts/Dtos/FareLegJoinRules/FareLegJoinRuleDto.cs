using System;

namespace Byd.GtfsManagement.Dtos.FareLegJoinRules;

// islevi: FareLegJoinRule satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS ucret etap birlestirme kurali cevap alanlarini tasir.</summary>
public class FareLegJoinRuleDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string? FromNetworkId { get; set; }
    public string? ToNetworkId { get; set; }
    public string? FromStopId { get; set; }
    public string? ToStopId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
