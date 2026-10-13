namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: FareTransferRule duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret aktarma kurali ortak istek alanlarini tasir.</summary>
public abstract class FareTransferRuleInputDto
{
    public string? FromLegGroupId { get; set; }
    public string? ToLegGroupId { get; set; }
    public string? GtfsFareProductId { get; set; }
    public int? TransferCount { get; set; }
    public int? DurationLimit { get; set; }
    public int? DurationLimitType { get; set; }
    public int FareTransferType { get; set; }
}
