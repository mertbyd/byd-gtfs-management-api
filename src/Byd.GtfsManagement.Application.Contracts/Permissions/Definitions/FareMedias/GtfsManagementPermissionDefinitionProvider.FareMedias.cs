using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareMedias izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret medyasi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // FareMedia izin agacini GTFS izin grubuna ekler.
    private static void AddFareMediaPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareMedias.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareMedias));
        node.AddChild(
            GtfsManagementPermissions.FareMedias.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareMediasView));
        node.AddChild(
            GtfsManagementPermissions.FareMedias.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareMediasManage));
    }
}
