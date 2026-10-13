using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareRules izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret kurali izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Ucret kurali okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddFareRulePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareRules.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareRules));
        node.AddChild(
            GtfsManagementPermissions.FareRules.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareRulesView));
        node.AddChild(
            GtfsManagementPermissions.FareRules.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareRulesManage));
    }
}
