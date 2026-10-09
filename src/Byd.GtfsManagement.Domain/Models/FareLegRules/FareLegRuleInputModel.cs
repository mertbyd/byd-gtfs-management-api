namespace Byd.GtfsManagement.Models.FareLegRules;

// islevi: FareLegRule olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret etap kurali ortak domain girdi modelini tasir.</summary>
public abstract class FareLegRuleInputModel
{
    public string? LegGroupId { get; set; }
    public string? NetworkId { get; set; }
    public string? FromAreaId { get; set; }
    public string? ToAreaId { get; set; }
    public string? FromTimeframeGroupId { get; set; }
    public string? ToTimeframeGroupId { get; set; }
    public string GtfsFareProductId { get; set; } = default!;
    public int? RulePriority { get; set; }
}
