using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: BookingRules izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS rezervasyon kurali izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // BookingRule izin agacini GTFS izin grubuna ekler.
    private static void AddBookingRulePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.BookingRules.Default,
            L(GtfsManagementLocalizationKeys.Permissions.BookingRules));
        node.AddChild(
            GtfsManagementPermissions.BookingRules.View,
            L(GtfsManagementLocalizationKeys.Permissions.BookingRulesView));
        node.AddChild(
            GtfsManagementPermissions.BookingRules.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.BookingRulesManage));
    }
}
