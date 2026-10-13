using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: FareTransferRule liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS ucret aktarma kurali filtrelerini tasir.</summary>
public class FareTransferRuleFilter
{
    [DataTable(FareTransferRuleFilterFields.FeedId, FareTransferRuleFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareTransferRuleFilterFields.FromLegGroupId, FareTransferRuleFilterFields.PropertyPaths.FromLegGroupId)]
    public DataTableFilterItem? FromLegGroupId { get; set; }

    [DataTable(FareTransferRuleFilterFields.ToLegGroupId, FareTransferRuleFilterFields.PropertyPaths.ToLegGroupId)]
    public DataTableFilterItem? ToLegGroupId { get; set; }

    [DataTable(FareTransferRuleFilterFields.TransferCount, FareTransferRuleFilterFields.PropertyPaths.TransferCount)]
    public DataTableFilterItem? TransferCount { get; set; }

    [DataTable(FareTransferRuleFilterFields.DurationLimit, FareTransferRuleFilterFields.PropertyPaths.DurationLimit)]
    public DataTableFilterItem? DurationLimit { get; set; }

    [DataTable(FareTransferRuleFilterFields.DurationLimitType, FareTransferRuleFilterFields.PropertyPaths.DurationLimitType)]
    public DataTableFilterItem? DurationLimitType { get; set; }

    [DataTable(FareTransferRuleFilterFields.FareTransferType, FareTransferRuleFilterFields.PropertyPaths.FareTransferType)]
    public DataTableFilterItem? FareTransferType { get; set; }

    [DataTable(FareTransferRuleFilterFields.GtfsFareProductId, FareTransferRuleFilterFields.PropertyPaths.GtfsFareProductId)]
    public DataTableFilterItem? GtfsFareProductId { get; set; }

    [DataTable(FareTransferRuleFilterFields.CreationTime, FareTransferRuleFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
