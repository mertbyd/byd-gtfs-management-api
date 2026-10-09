using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Stop izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Stop izin agacini tanimlar.
    private static void AddStopPermissions(PermissionGroupDefinition group)
    {
        var stop = group.AddPermission(
            GtfsManagementPermissions.Stops.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Stops));
        stop.AddChild(
            GtfsManagementPermissions.Stops.View,
            L(GtfsManagementLocalizationKeys.Permissions.StopsView));
        stop.AddChild(
            GtfsManagementPermissions.Stops.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.StopsManage));
    }
}
