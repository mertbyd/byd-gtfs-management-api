using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Translations izin dugumlerini ABP grubuna ekler.
// sistemdeki gorevi: View ve Manage eylemlerini lokalize edilmis olarak sunar.
/// <summary>GTFS ceviri izin agacini tanimlar.</summary>
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Ceviri okuma ve yonetim izinlerini ana gruba ekler.
    private static void AddTranslationPermissions(PermissionGroupDefinition group)
    {
        var node = group.AddPermission(
            GtfsManagementPermissions.Translations.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Translations));
        node.AddChild(
            GtfsManagementPermissions.Translations.View,
            L(GtfsManagementLocalizationKeys.Permissions.TranslationsView));
        node.AddChild(
            GtfsManagementPermissions.Translations.Manage,
            L(GtfsManagementLocalizationKeys.Permissions.TranslationsManage));
    }
}
