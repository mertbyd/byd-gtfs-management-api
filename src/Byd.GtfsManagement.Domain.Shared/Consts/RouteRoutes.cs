namespace Byd.GtfsManagement.Consts;

// islevi: Route HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class RouteRoutes
{
    public const string Root = "api/gtfs-management/routes";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
    public const string Detail = "{id:guid}/detail";
}
