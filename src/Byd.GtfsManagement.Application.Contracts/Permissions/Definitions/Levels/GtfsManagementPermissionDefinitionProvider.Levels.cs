using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Levels izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS kat izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Kat okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddLevelPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Levels.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Levels));
        node.AddChild(
            GtfsManagementPermissions.Levels.View,
            L(GtfsManagementLocalizationKeys.Permissions.LevelsView));
        node.AddChild(
            GtfsManagementPermissions.Levels.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.LevelsManage));
    }
}
