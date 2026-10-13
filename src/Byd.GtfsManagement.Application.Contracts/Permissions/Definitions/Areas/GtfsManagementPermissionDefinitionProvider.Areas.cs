using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Areas izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS bolge izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Bolge okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddAreaPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Areas.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Areas));
        node.AddChild(
            GtfsManagementPermissions.Areas.View,
            L(GtfsManagementLocalizationKeys.Permissions.AreasView));
        node.AddChild(
            GtfsManagementPermissions.Areas.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.AreasManage));
    }
}
