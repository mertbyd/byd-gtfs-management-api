using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Calendars izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Calendars izin agacini tanimlar.
    private static void AddCalendarPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Calendars.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Calendars));
        node.AddChild(
            GtfsManagementPermissions.Calendars.View,
            L(GtfsManagementLocalizationKeys.Permissions.CalendarsView));
        node.AddChild(
            GtfsManagementPermissions.Calendars.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.CalendarsManage));
    }
}
