using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: RiderCategories izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS yolcu kategorisi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // RiderCategory izin agacini GTFS izin grubuna ekler.
    private static void AddRiderCategoryPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.RiderCategories.Default,
            L(GtfsManagementLocalizationKeys.Permissions.RiderCategories));
        node.AddChild(
            GtfsManagementPermissions.RiderCategories.View,
            L(GtfsManagementLocalizationKeys.Permissions.RiderCategoriesView));
        node.AddChild(
            GtfsManagementPermissions.RiderCategories.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.RiderCategoriesManage));
    }
}
