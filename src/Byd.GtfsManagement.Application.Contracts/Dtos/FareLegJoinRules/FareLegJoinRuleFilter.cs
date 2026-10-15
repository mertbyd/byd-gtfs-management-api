using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareLegJoinRules;

// islevi: FareLegJoinRule liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ucret etap birlestirme kurali filtrelerini tasir.</summary>
public class FareLegJoinRuleFilter : IFeedFilter
{
    [DataTable(FareLegJoinRuleFilterFields.FeedId, FareLegJoinRuleFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareLegJoinRuleFilterFields.FromNetworkId, FareLegJoinRuleFilterFields.PropertyPaths.FromNetworkId)]
    public DataTableFilterItem? FromNetworkId { get; set; }

    [DataTable(FareLegJoinRuleFilterFields.ToNetworkId, FareLegJoinRuleFilterFields.PropertyPaths.ToNetworkId)]
    public DataTableFilterItem? ToNetworkId { get; set; }

    [DataTable(FareLegJoinRuleFilterFields.FromStopId, FareLegJoinRuleFilterFields.PropertyPaths.FromStopId)]
    public DataTableFilterItem? FromStopId { get; set; }

    [DataTable(FareLegJoinRuleFilterFields.ToStopId, FareLegJoinRuleFilterFields.PropertyPaths.ToStopId)]
    public DataTableFilterItem? ToStopId { get; set; }

    [DataTable(FareLegJoinRuleFilterFields.CreationTime, FareLegJoinRuleFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
