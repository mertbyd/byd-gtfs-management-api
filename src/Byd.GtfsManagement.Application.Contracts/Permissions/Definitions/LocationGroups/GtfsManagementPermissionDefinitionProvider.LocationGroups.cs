using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: LocationGroups izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS konum grubu izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // LocationGroup izin agacini GTFS izin grubuna ekler.
    private static void AddLocationGroupPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.LocationGroups.Default,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroups));
        node.AddChild(
            GtfsManagementPermissions.LocationGroups.View,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroupsView));
        node.AddChild(
            GtfsManagementPermissions.LocationGroups.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroupsManage));
    }
}
