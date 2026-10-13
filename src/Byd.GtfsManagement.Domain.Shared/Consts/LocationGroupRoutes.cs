namespace Byd.GtfsManagement.Consts;

// islevi: LocationGroup HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS konum grubu rota yollarini tanimlar.</summary>
public static class LocationGroupRoutes
{
    public const string Root = "api/gtfs-management/location-groups";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
