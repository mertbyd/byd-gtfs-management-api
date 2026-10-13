namespace Byd.GtfsManagement.Consts;

// islevi: Agency HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class AgencyRoutes
{
    public const string Root = "api/gtfs-management/agencies";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
