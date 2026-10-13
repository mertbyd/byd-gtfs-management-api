using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Timeframes izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS zaman araligi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Timeframe izin agacini GTFS izin grubuna ekler.
    private static void AddTimeframePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Timeframes.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Timeframes));
        node.AddChild(
            GtfsManagementPermissions.Timeframes.View,
            L(GtfsManagementLocalizationKeys.Permissions.TimeframesView));
        node.AddChild(
            GtfsManagementPermissions.Timeframes.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.TimeframesManage));
    }
}
