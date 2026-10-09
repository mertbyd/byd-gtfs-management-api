using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Transfers izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS aktarma izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Aktarma okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddTransferPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Transfers.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Transfers));
        node.AddChild(
            GtfsManagementPermissions.Transfers.View,
            L(GtfsManagementLocalizationKeys.Permissions.TransfersView));
        node.AddChild(
            GtfsManagementPermissions.Transfers.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.TransfersManage));
    }
}
