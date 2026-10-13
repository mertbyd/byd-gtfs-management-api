namespace Byd.GtfsManagement.Consts;

// islevi: FareAttribute HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret ozelligi rota yollarini tanimlar.</summary>
public static class FareAttributeRoutes
{
    public const string Root = "api/gtfs-management/fare-attributes";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
