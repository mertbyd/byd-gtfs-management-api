using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareLegRules;

// islevi: fare_leg_rules.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareLegRule ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareLegRules, GtfsImportOrders.FareLegRule)]
public class FareLegRuleImportDto
{
    [Name(GtfsColumnNames.LegGroupId)]
    public string? LegGroupId { get; set; }
    [Name(GtfsColumnNames.NetworkId)]
    public string? NetworkId { get; set; }
    [Name(GtfsColumnNames.FromAreaId)]
    public string? FromAreaId { get; set; }
    [Name(GtfsColumnNames.ToAreaId)]
    public string? ToAreaId { get; set; }
    [Name(GtfsColumnNames.FromTimeframeGroupId)]
    public string? FromTimeframeGroupId { get; set; }
    [Name(GtfsColumnNames.ToTimeframeGroupId)]
    public string? ToTimeframeGroupId { get; set; }
    [Name(GtfsColumnNames.FareProductId)]
    public string GtfsFareProductId { get; set; } = default!;
    [Name(GtfsColumnNames.RulePriority)]
    public int? RulePriority { get; set; }
}
