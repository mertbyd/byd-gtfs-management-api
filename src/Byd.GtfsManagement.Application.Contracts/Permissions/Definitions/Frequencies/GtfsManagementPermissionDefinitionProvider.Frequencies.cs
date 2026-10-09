using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Frequencies izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini localized olarak sunar.
/// <summary>Sefer sikligi izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Sefer sikligi okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddFrequencyPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Frequencies.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Frequencies));
        node.AddChild(
            GtfsManagementPermissions.Frequencies.View,
            L(GtfsManagementLocalizationKeys.Permissions.FrequenciesView));
        node.AddChild(
            GtfsManagementPermissions.Frequencies.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.FrequenciesManage));
    }
}
