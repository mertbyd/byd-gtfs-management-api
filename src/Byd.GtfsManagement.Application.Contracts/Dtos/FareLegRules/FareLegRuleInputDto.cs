namespace Byd.GtfsManagement.Dtos.FareLegRules;

// islevi: FareLegRule duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ucret etap kurali ortak istek alanlarini tasir.</summary>
public abstract class FareLegRuleInputDto
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
