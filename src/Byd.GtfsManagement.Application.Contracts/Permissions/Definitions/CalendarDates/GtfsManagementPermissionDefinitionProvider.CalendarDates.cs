using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: CalendarDates izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // CalendarDates izin agacini tanimlar.
    private static void AddCalendarDatePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.CalendarDates.Default,
            L(GtfsManagementLocalizationKeys.Permissions.CalendarDates));
        node.AddChild(
            GtfsManagementPermissions.CalendarDates.View,
            L(GtfsManagementLocalizationKeys.Permissions.CalendarDatesView));
        node.AddChild(
            GtfsManagementPermissions.CalendarDates.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.CalendarDatesManage));
    }
}
