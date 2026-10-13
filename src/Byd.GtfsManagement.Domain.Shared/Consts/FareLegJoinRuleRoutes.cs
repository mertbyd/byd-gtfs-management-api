namespace Byd.GtfsManagement.Consts;

// islevi: FareLegJoinRule HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret etap birlestirme kurali rota yollarini tanimlar.</summary>
public static class FareLegJoinRuleRoutes
{
    public const string Root = "api/gtfs-management/fare-leg-join-rules";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
