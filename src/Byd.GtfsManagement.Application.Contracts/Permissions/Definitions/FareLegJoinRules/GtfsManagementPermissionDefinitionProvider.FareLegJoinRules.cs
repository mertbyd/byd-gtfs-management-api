using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareLegJoinRules izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret etap birlestirme kurali izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // FareLegJoinRule izin agacini GTFS izin grubuna ekler.
    private static void AddFareLegJoinRulePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareLegJoinRules.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegJoinRules));
        node.AddChild(
            GtfsManagementPermissions.FareLegJoinRules.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegJoinRulesView));
        node.AddChild(
            GtfsManagementPermissions.FareLegJoinRules.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegJoinRulesManage));
    }
}
