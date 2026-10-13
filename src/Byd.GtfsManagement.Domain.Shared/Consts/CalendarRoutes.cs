namespace Byd.GtfsManagement.Consts;

// islevi: Calendar HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class CalendarRoutes
{
    public const string Root = "api/gtfs-management/calendars";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
