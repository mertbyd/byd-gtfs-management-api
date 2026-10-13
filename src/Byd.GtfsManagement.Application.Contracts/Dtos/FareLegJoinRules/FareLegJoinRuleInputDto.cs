namespace Byd.GtfsManagement.Dtos.FareLegJoinRules;

// islevi: FareLegJoinRule duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret etap birlestirme kurali ortak istek alanlarini tasir.</summary>
public abstract class FareLegJoinRuleInputDto
{
    public string? FromNetworkId { get; set; }
    public string? ToNetworkId { get; set; }
    public string? FromStopId { get; set; }
    public string? ToStopId { get; set; }
}
