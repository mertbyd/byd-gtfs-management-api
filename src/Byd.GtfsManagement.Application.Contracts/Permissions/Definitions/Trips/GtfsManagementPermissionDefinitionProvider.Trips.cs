using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Trip izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Trip izin agacini tanimlar.
    private static void AddTripPermissions(PermissionGroupDefinition group)
    {
        var trip = group.AddPermission(
            GtfsManagementPermissions.Trips.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Trips));
        trip.AddChild(
            GtfsManagementPermissions.Trips.View,
            L(GtfsManagementLocalizationKeys.Permissions.TripsView));
        trip.AddChild(
            GtfsManagementPermissions.Trips.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.TripsManage));
    }
}
