namespace Byd.GtfsManagement.Consts;

// islevi: StopTime HTTP yollarini tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
/// <summary>StopTime HTTP yollarini tanimlar.</summary>
public static class StopTimeRoutes
{
    public const string Root = "api/gtfs-management/stop-times";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
