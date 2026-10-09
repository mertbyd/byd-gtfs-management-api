namespace Byd.GtfsManagement.Consts;

// islevi: Service HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class ServiceRoutes
{
    public const string Root = "api/gtfs-management/services";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
