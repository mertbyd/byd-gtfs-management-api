namespace Byd.GtfsManagement.Consts;

// islevi: LocationGroupStop HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS konum grubu durak iliskisi rota yollarini tanimlar.</summary>
public static class LocationGroupStopRoutes
{
    public const string Root = "api/gtfs-management/location-group-stops";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
