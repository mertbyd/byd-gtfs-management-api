namespace Byd.GtfsManagement.Models.FareLegJoinRules;

// islevi: FareLegJoinRule olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret etap birlestirme kurali ortak domain girdi modelini tasir.</summary>
public abstract class FareLegJoinRuleInputModel
{
    public string? FromNetworkId { get; set; }
    public string? ToNetworkId { get; set; }
    public string? FromStopId { get; set; }
    public string? ToStopId { get; set; }
}
