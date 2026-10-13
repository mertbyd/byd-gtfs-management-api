namespace Byd.GtfsManagement.Consts;

// islevi: FeedInfo HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared uzerinden sunar.
/// <summary>GTFS yayimci bilgisi HTTP yollarini tanimlar.</summary>
public static class FeedInfoRoutes
{
    public const string Root = "api/gtfs-management/feed-infos";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
