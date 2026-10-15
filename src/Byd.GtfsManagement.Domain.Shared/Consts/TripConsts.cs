namespace Byd.GtfsManagement.Consts;

// islevi: trips.txt kayitlarinin tablo, alan uzunlugu ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: Entity, EF eslemesi ve validator'in ayni GTFS sefer sozlesmesini okumasini saglar.
public static class TripConsts
{
    public const string TableName = "Trips";
    public const string GtfsTripIdIndex = "IX_Trips_Feed_GtfsTripId";
    public const string RouteIndex = "IX_Trips_RouteId";
    public const string ServiceIndex = "IX_Trips_ServiceId";

    public const int MaxGtfsTripIdLength = 100;
    public const int MaxHeadsignLength = 200;
    public const int MaxShortNameLength = 50;
    public const int MaxBlockIdLength = 100;
}
