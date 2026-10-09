using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Pathways izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS yuruyus yolu izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Yuruyus yolu okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddPathwayPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Pathways.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Pathways));
        node.AddChild(
            GtfsManagementPermissions.Pathways.View,
            L(GtfsManagementLocalizationKeys.Permissions.PathwaysView));
        node.AddChild(
            GtfsManagementPermissions.Pathways.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.PathwaysManage));
    }
}
