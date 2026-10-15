namespace Byd.GtfsManagement.Consts;

// islevi: GTFS service_id kayitlarinin tablo, alan uzunlugu ve tekillik indeks adlarini tanimlar.
// sistemdeki gorevi: calendar.txt ve calendar_dates.txt kayitlarinin bagli oldugu kimligi sabitler.
public static class ServiceConsts
{
    public const string TableName = "Services";
    public const string GtfsServiceIdIndex = "IX_Services_Feed_GtfsServiceId";

    public const int MaxGtfsServiceIdLength = 100;
}
