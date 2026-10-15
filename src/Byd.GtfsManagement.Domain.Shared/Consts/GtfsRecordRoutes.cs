namespace Byd.GtfsManagement.Consts;

// islevi: Feed kapsamli GTFS yonetim okumalarinin ortak rota parcalarini bildirir.
// sistemdeki gorevi: Normal yayin sorgularindan ayri, yetkili taslak ve gecmis okunmasini sunar.
public static class GtfsRecordRoutes
{
    public const string ForFeed = "feeds/{feedId:guid}/{id:guid}";
    public const string ListForFeed = "feeds/{feedId:guid}/filtered-list";
    public const string DetailForFeed = ForFeed + "/detail";
    public const string MapForFeed = "feeds/{feedId:guid}/map";
    public const string DeparturesForFeed = ForFeed + "/departures";
}
