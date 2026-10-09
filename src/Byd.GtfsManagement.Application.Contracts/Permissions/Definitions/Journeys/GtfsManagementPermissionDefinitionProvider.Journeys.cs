using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;

namespace Byd.GtfsManagement.Permissions;

// islevi: Yolculuk planlama izin agacini ABP permission grubuna ekler.
// sistemdeki gorevi: Izin adlarini Domain.Shared sabitlerinden, gorunen adlari lokalizasyon anahtarlarindan alir.
public partial class GtfsManagementPermissionDefinitionProvider
{
    // Journey izin agacini tanimlar.
    private static void AddJourneyPermissions(PermissionGroupDefinition group)
    {
        var journey = group.AddPermission(
            GtfsManagementPermissions.Journeys.Default,
            L(GtfsManagementLocalizationKeys.Permissions.Journeys));
        journey.AddChild(
            GtfsManagementPermissions.Journeys.View,
            L(GtfsManagementLocalizationKeys.Permissions.JourneysView));
    }
}
