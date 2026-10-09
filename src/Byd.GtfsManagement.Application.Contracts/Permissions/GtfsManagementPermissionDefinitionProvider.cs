using Byd.GtfsManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Byd.GtfsManagement.Permissions;

// islevi: GTFS izin grubunu ve her ozelligin izin agacini ABP'ye tanitir.
// sistemdeki gorevi: Ozellik izinleri ayri partial dosyalarda tanimlanir; bu dosya grubu kurar ve sirayla ekler.
public partial class GtfsManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    // GTFS izin grubunu kurar ve ozellik izinlerini ekler.
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(
            GtfsManagementPermissions.GroupName,
            L(GtfsManagementLocalizationKeys.Permissions.Group));
        AddAgencyPermissions(group);
        AddStopPermissions(group);
        AddRoutePermissions(group);
        AddServicePermissions(group);
        AddCalendarPermissions(group);
        AddCalendarDatePermissions(group);
        AddTripPermissions(group);
        AddStopTimePermissions(group);
        AddFrequencyPermissions(group);
        AddTransferPermissions(group);
        AddShapePermissions(group);
        AddFeedInfoPermissions(group);
        AddLevelPermissions(group);
        AddPathwayPermissions(group);
        AddFareAttributePermissions(group);
        AddFareRulePermissions(group);
        AddTranslationPermissions(group);
        AddAttributionPermissions(group);
        AddAreaPermissions(group);
        AddTimeframePermissions(group);
        AddRiderCategoryPermissions(group);
        AddFareMediaPermissions(group);
        AddFareProductPermissions(group);
        AddFareLegRulePermissions(group);
        AddFareLegJoinRulePermissions(group);
        AddFareTransferRulePermissions(group);
        AddStopAreaPermissions(group);
        AddNetworkPermissions(group);
        AddRouteNetworkPermissions(group);
        AddLocationGroupPermissions(group);
        AddLocationGroupStopPermissions(group);
        AddBookingRulePermissions(group);
    }

    // Izin adini modulun lokalizasyon kaynagindan cozer.
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GtfsManagementResource>(name);
    }
}
