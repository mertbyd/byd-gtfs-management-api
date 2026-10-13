using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: LocationGroupStops izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS konum grubu durak iliskisi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // LocationGroupStop izin agacini GTFS izin grubuna ekler.
    private static void AddLocationGroupStopPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.LocationGroupStops.Default,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroupStops));
        node.AddChild(
            GtfsManagementPermissions.LocationGroupStops.View,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroupStopsView));
        node.AddChild(
            GtfsManagementPermissions.LocationGroupStops.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.LocationGroupStopsManage));
    }
}
