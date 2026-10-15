using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Feed izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View, Manage ve Export eylemlerini localized olarak sunar.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Feed izin agacini tanimlar.
    private static void AddFeedPermissions(PermissionGroupDefinition group)
    {
        var feed = group.AddPermission(
            GtfsManagementPermissions.Feeds.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Feeds));
        feed.AddChild(
            GtfsManagementPermissions.Feeds.View,
            L(GtfsManagementLocalizationKeys.Permissions.FeedsView));
        feed.AddChild(
            GtfsManagementPermissions.Feeds.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FeedsManage));
        feed.AddChild(
            GtfsManagementPermissions.Feeds.Export,
            L(GtfsManagementLocalizationKeys.Permissions.FeedsExport));
    }
}
