using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: StopTimes izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
/// <summary>StopTime izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // StopTime goruntuleme ve yonetim izinlerini ana gruba ekler.
    private static void AddStopTimePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.StopTimes.Default,
            L(GtfsManagementLocalizationKeys.Permissions.StopTimes));
        node.AddChild(
            GtfsManagementPermissions.StopTimes.View,
            L(GtfsManagementLocalizationKeys.Permissions.StopTimesView));
        node.AddChild(
            GtfsManagementPermissions.StopTimes.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.StopTimesManage));
    }
}
