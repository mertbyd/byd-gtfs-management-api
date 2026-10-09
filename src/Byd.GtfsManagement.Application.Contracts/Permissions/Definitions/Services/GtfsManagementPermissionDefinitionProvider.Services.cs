using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Services izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Services izin agacini tanimlar.
    private static void AddServicePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Services.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Services));
        node.AddChild(
            GtfsManagementPermissions.Services.View,
            L(GtfsManagementLocalizationKeys.Permissions.ServicesView));
        node.AddChild(
            GtfsManagementPermissions.Services.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.ServicesManage));
    }
}
