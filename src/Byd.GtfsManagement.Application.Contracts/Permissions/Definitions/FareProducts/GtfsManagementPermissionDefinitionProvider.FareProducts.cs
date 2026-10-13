using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareProducts izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret urunu izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // FareProduct izin agacini GTFS izin grubuna ekler.
    private static void AddFareProductPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareProducts.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareProducts));
        node.AddChild(
            GtfsManagementPermissions.FareProducts.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareProductsView));
        node.AddChild(
            GtfsManagementPermissions.FareProducts.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareProductsManage));
    }
}
