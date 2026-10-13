namespace Byd.GtfsManagement.Consts;

// islevi: location_group_stops.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS konum grubu durak iliskisi sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS konum grubu durak iliskisi sabitlerini tanimlar.</summary>
public static class LocationGroupStopConsts
{
    public const string TableName = "LocationGroupStops";
    public const string KeyIndex = "IX_LocationGroupStops_FeedId_GtfsLocationGroupId_GtfsStopId";
    public const int MaxGtfsLocationGroupIdLength = 256;
    public const int MaxGtfsStopIdLength = 256;
}
