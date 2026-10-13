using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareLegRules izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret etap kurali izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // FareLegRule izin agacini GTFS izin grubuna ekler.
    private static void AddFareLegRulePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareLegRules.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegRules));
        node.AddChild(
            GtfsManagementPermissions.FareLegRules.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegRulesView));
        node.AddChild(
            GtfsManagementPermissions.FareLegRules.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareLegRulesManage));
    }
}
