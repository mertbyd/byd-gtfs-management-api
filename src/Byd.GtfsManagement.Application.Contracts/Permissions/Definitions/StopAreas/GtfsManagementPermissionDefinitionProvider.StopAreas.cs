using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: StopAreas izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS durak bolge iliskisi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // StopArea izin agacini GTFS izin grubuna ekler.
    private static void AddStopAreaPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.StopAreas.Default,
            L(GtfsManagementLocalizationKeys.Permissions.StopAreas));
        node.AddChild(
            GtfsManagementPermissions.StopAreas.View,
            L(GtfsManagementLocalizationKeys.Permissions.StopAreasView));
        node.AddChild(
            GtfsManagementPermissions.StopAreas.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.StopAreasManage));
    }
}
