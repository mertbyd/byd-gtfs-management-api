namespace Byd.GtfsManagement.Consts;

// islevi: Stop HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class StopRoutes
{
    public const string Root = "api/gtfs-management/stops";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
