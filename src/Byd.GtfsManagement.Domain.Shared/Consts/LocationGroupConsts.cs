namespace Byd.GtfsManagement.Consts;

// islevi: location_groups.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS konum grubu sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS konum grubu sabitlerini tanimlar.</summary>
public static class LocationGroupConsts
{
    public const string TableName = "LocationGroups";
    public const string KeyIndex = "IX_LocationGroups_FeedId_GtfsLocationGroupId";
    public const int MaxGtfsLocationGroupIdLength = 256;
    public const int MaxLocationGroupNameLength = 256;
}
