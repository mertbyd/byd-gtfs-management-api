using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareLegRules;

// islevi: FareLegRule liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ucret etap kurali filtrelerini tasir.</summary>
public class FareLegRuleFilter
{
    [DataTable(FareLegRuleFilterFields.FeedId, FareLegRuleFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareLegRuleFilterFields.LegGroupId, FareLegRuleFilterFields.PropertyPaths.LegGroupId)]
    public DataTableFilterItem? LegGroupId { get; set; }

    [DataTable(FareLegRuleFilterFields.NetworkId, FareLegRuleFilterFields.PropertyPaths.NetworkId)]
    public DataTableFilterItem? NetworkId { get; set; }

    [DataTable(FareLegRuleFilterFields.FromAreaId, FareLegRuleFilterFields.PropertyPaths.FromAreaId)]
    public DataTableFilterItem? FromAreaId { get; set; }

    [DataTable(FareLegRuleFilterFields.ToAreaId, FareLegRuleFilterFields.PropertyPaths.ToAreaId)]
    public DataTableFilterItem? ToAreaId { get; set; }

    [DataTable(FareLegRuleFilterFields.FromTimeframeGroupId, FareLegRuleFilterFields.PropertyPaths.FromTimeframeGroupId)]
    public DataTableFilterItem? FromTimeframeGroupId { get; set; }

    [DataTable(FareLegRuleFilterFields.ToTimeframeGroupId, FareLegRuleFilterFields.PropertyPaths.ToTimeframeGroupId)]
    public DataTableFilterItem? ToTimeframeGroupId { get; set; }

    [DataTable(FareLegRuleFilterFields.GtfsFareProductId, FareLegRuleFilterFields.PropertyPaths.GtfsFareProductId)]
    public DataTableFilterItem? GtfsFareProductId { get; set; }

    [DataTable(FareLegRuleFilterFields.RulePriority, FareLegRuleFilterFields.PropertyPaths.RulePriority)]
    public DataTableFilterItem? RulePriority { get; set; }

    [DataTable(FareLegRuleFilterFields.CreationTime, FareLegRuleFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
