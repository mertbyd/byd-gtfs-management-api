using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Attributions izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS atif izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Atif okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddAttributionPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Attributions.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Attributions));
        node.AddChild(
            GtfsManagementPermissions.Attributions.View,
            L(GtfsManagementLocalizationKeys.Permissions.AttributionsView));
        node.AddChild(
            GtfsManagementPermissions.Attributions.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.AttributionsManage));
    }
}
