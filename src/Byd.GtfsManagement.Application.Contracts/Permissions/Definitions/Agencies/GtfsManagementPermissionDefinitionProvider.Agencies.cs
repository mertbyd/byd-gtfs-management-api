using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Agency izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Agency izin agacini tanimlar.
    private static void AddAgencyPermissions(PermissionGroupDefinition group)
    {
        var agency = group.AddPermission(
            GtfsManagementPermissions.Agencies.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Agencies));
        agency.AddChild(
            GtfsManagementPermissions.Agencies.View,
            L(GtfsManagementLocalizationKeys.Permissions.AgenciesView));
        agency.AddChild(
            GtfsManagementPermissions.Agencies.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.AgenciesManage));
    }
}
