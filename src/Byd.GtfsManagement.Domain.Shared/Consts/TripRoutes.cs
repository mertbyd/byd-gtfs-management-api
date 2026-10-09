namespace Byd.GtfsManagement.Consts;

// islevi: Trip HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class TripRoutes
{
    public const string Root = "api/gtfs-management/trips";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
