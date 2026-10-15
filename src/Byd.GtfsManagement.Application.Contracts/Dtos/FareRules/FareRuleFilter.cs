using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareRules;

// islevi: FareRule liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Ucret kurali sorgusunun izinli alanlarini belirler.
/// <summary>GTFS ucret kurali filtrelerini tasir.</summary>
public class FareRuleFilter : IFeedFilter
{
    [DataTable(FareRuleFilterFields.FeedId, FareRuleFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareRuleFilterFields.FareAttributeId, FareRuleFilterFields.PropertyPaths.FareAttributeId)]
    public DataTableFilterItem? FareAttributeId { get; set; }

    [DataTable(FareRuleFilterFields.RouteId, FareRuleFilterFields.PropertyPaths.RouteId)]
    public DataTableFilterItem? RouteId { get; set; }

    [DataTable(FareRuleFilterFields.OriginId, FareRuleFilterFields.PropertyPaths.OriginId)]
    public DataTableFilterItem? OriginId { get; set; }

    [DataTable(FareRuleFilterFields.DestinationId, FareRuleFilterFields.PropertyPaths.DestinationId)]
    public DataTableFilterItem? DestinationId { get; set; }

    [DataTable(FareRuleFilterFields.ContainsId, FareRuleFilterFields.PropertyPaths.ContainsId)]
    public DataTableFilterItem? ContainsId { get; set; }

    [DataTable(FareRuleFilterFields.CreationTime, FareRuleFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
