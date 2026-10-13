namespace Byd.GtfsManagement.Consts;

// islevi: FareRule HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret kurali rota yollarini tanimlar.</summary>
public static class FareRuleRoutes
{
    public const string Root = "api/gtfs-management/fare-rules";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
