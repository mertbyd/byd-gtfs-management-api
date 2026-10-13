using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: FareAttributes izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ucret ozelligi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Ucret ozelligi okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddFareAttributePermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.FareAttributes.Default,
            L(GtfsManagementLocalizationKeys.Permissions.FareAttributes));
        node.AddChild(
            GtfsManagementPermissions.FareAttributes.View,
            L(GtfsManagementLocalizationKeys.Permissions.FareAttributesView));
        node.AddChild(
            GtfsManagementPermissions.FareAttributes.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FareAttributesManage));
    }
}
