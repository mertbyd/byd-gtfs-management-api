namespace Byd.GtfsManagement.Consts;

// islevi: Network HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ulasim agi rota yollarini tanimlar.</summary>
public static class NetworkRoutes
{
    public const string Root = "api/gtfs-management/networks";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
