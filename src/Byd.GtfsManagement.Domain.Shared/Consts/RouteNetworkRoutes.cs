namespace Byd.GtfsManagement.Consts;

// islevi: RouteNetwork HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS hat ag iliskisi rota yollarini tanimlar.</summary>
public static class RouteNetworkRoutes
{
    public const string Root = "api/gtfs-management/route-networks";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
