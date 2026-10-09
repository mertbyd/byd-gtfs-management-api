namespace Byd.GtfsManagement.Models.FareTransferRules;

// islevi: FareTransferRule olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret aktarma kurali ortak domain girdi modelini tasir.</summary>
public abstract class FareTransferRuleInputModel
{
    public string? FromLegGroupId { get; set; }
    public string? ToLegGroupId { get; set; }
    public string? GtfsFareProductId { get; set; }
    public int? TransferCount { get; set; }
    public int? DurationLimit { get; set; }
    public int? DurationLimitType { get; set; }
    public int FareTransferType { get; set; }
}
