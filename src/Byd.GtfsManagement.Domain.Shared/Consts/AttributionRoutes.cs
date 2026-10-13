namespace Byd.GtfsManagement.Consts;

// islevi: Attribution HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS atif rota yollarini tanimlar.</summary>
public static class AttributionRoutes
{
    public const string Root = "api/gtfs-management/attributions";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
