using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareTransferRules izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret aktarma kurali izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // FareTransferRule izin agacini GTFS izin grubuna ekler.
    private static void AddFareTransferRulePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareTransferRules.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareTransferRules));
        node.AddChild(
            GtfsManagementPermissions.FareTransferRules.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareTransferRulesView));
        node.AddChild(
            GtfsManagementPermissions.FareTransferRules.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareTransferRulesManage));
    }
}
