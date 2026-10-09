namespace Byd.GtfsManagement.Consts;

// islevi: CalendarDate HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class CalendarDateRoutes
{
    public const string Root = "api/gtfs-management/calendar-dates";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
