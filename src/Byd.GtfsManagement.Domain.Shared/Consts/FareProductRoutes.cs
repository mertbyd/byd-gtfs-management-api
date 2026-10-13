namespace Byd.GtfsManagement.Consts;

// islevi: FareProduct HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret urunu rota yollarini tanimlar.</summary>
public static class FareProductRoutes
{
    public const string Root = "api/gtfs-management/fare-products";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
