namespace Byd.GtfsManagement.Consts;

// islevi: routes.txt kayitlarinin tablo, alan uzunlugu, renk bicimi ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: Entity, EF eslemesi ve validator'in ayni GTFS guzergah sozlesmesini okumasini saglar.
public static class RouteConsts
{
    public const string TableName = "Routes";
    public const string GtfsRouteIdIndex = "IX_Routes_Feed_GtfsRouteId";
    public const string AgencyIndex = "IX_Routes_AgencyId";

    public const int MaxGtfsRouteIdLength = 100;
    public const int MaxShortNameLength = 50;
    public const int MaxLongNameLength = 200;
    public const int MaxDescriptionLength = 500;
    public const int MaxUrlLength = 500;
    public const int ColorLength = 6;

    // GTFS renk alanlari bas karakteri olmadan alti haneli onaltilik degerdir.
    public const string ColorPattern = "^[0-9A-Fa-f]{6}$";
}
