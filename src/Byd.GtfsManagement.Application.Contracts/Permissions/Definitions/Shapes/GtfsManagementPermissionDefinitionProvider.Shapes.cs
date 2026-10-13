using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Shapes izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS sekil izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Sekil okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddShapePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Shapes.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Shapes));
        node.AddChild(
            GtfsManagementPermissions.Shapes.View,
            L(GtfsManagementLocalizationKeys.Permissions.ShapesView));
        node.AddChild(
            GtfsManagementPermissions.Shapes.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.ShapesManage));
    }
}
