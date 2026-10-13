namespace Byd.GtfsManagement.Consts;

// islevi: Area HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS bolge rota yollarini tanimlar.</summary>
public static class AreaRoutes
{
    public const string Root = "api/gtfs-management/areas";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
