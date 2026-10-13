namespace Byd.GtfsManagement.Consts;

// islevi: StopArea HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS durak bolge iliskisi rota yollarini tanimlar.</summary>
public static class StopAreaRoutes
{
    public const string Root = "api/gtfs-management/stop-areas";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
