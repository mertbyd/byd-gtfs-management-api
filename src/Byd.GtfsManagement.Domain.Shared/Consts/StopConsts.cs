namespace Byd.GtfsManagement.Consts;

// islevi: stops.txt kayitlarinin tablo, alan uzunlugu, koordinat sinirlari ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: Entity, EF eslemesi ve validator'in ayni GTFS durak sozlesmesini okumasini saglar.
public static class StopConsts
{
    public const string TableName = "Stops";
    public const string GtfsStopIdIndex = "IX_Stops_Feed_GtfsStopId";
    public const string ParentStationIndex = "IX_Stops_ParentStationId";

    // Aktif Feed duraklarinin arama indeksi; tablonun sema ve adini izler.
    public const string SearchIndexName = "gtfs_management_stops";

    public const int MaxGtfsStopIdLength = 100;
    public const int MaxCodeLength = 100;
    public const int MaxNameLength = 200;
    public const int MaxTtsNameLength = 200;
    public const int MaxDescriptionLength = 500;
    public const int MaxZoneIdLength = 100;
    public const int MaxUrlLength = 500;
    public const int MaxTimezoneLength = 100;
    public const int MaxPlatformCodeLength = 100;

    public const double MinLatitude = -90;
    public const double MaxLatitude = 90;
    public const double MinLongitude = -180;
    public const double MaxLongitude = 180;

    public const int DefaultDepartureLimit = 20;
    public const int MaxDepartureLimit = 200;
}
