using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: RouteNetworks izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS hat ag iliskisi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // RouteNetwork izin agacini GTFS izin grubuna ekler.
    private static void AddRouteNetworkPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.RouteNetworks.Default,
            L(GtfsManagementLocalizationKeys.Permissions.RouteNetworks));
        node.AddChild(
            GtfsManagementPermissions.RouteNetworks.View,
            L(GtfsManagementLocalizationKeys.Permissions.RouteNetworksView));
        node.AddChild(
            GtfsManagementPermissions.RouteNetworks.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.RouteNetworksManage));
    }
}
