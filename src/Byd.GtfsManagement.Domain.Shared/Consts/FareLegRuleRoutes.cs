namespace Byd.GtfsManagement.Consts;

// islevi: FareLegRule HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret etap kurali rota yollarini tanimlar.</summary>
public static class FareLegRuleRoutes
{
    public const string Root = "api/gtfs-management/fare-leg-rules";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
