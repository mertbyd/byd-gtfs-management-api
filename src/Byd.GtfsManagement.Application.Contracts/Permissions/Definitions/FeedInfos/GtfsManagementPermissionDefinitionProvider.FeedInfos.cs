using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FeedInfos izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS yayimci bilgisi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Yayimci bilgisi okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddFeedInfoPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FeedInfos.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FeedInfos));
        node.AddChild(
            GtfsManagementPermissions.FeedInfos.View,
            L(GtfsManagementLocalizationKeys.Permissions.FeedInfosView));
        node.AddChild(
            GtfsManagementPermissions.FeedInfos.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FeedInfosManage));
    }
}
