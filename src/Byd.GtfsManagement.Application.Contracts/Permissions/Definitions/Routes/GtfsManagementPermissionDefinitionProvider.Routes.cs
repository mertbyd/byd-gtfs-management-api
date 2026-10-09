using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Route izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Route izin agacini tanimlar.
    private static void AddRoutePermissions(PermissionGroupDefinition group)
    {
        var route = group.AddPermission(
            GtfsManagementPermissions.Routes.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Routes));
        route.AddChild(
            GtfsManagementPermissions.Routes.View,
            L(GtfsManagementLocalizationKeys.Permissions.RoutesView));
        route.AddChild(
            GtfsManagementPermissions.Routes.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.RoutesManage));
    }
}
