using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Networks izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ulasim agi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Network izin agacini GTFS izin grubuna ekler.
    private static void AddNetworkPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Networks.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Networks));
        node.AddChild(
            GtfsManagementPermissions.Networks.View,
            L(GtfsManagementLocalizationKeys.Permissions.NetworksView));
        node.AddChild(
            GtfsManagementPermissions.Networks.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.NetworksManage));
    }
}
