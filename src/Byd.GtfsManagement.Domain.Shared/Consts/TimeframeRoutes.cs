namespace Byd.GtfsManagement.Consts;

// islevi: Timeframe HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS zaman araligi rota yollarini tanimlar.</summary>
public static class TimeframeRoutes
{
    public const string Root = "api/gtfs-management/timeframes";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
