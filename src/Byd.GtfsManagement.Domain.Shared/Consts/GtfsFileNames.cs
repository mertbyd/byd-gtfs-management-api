namespace Byd.GtfsManagement.Consts;

// islevi: GTFS ZIP'inde veritabanina aktarilan dosyalarin standart adlarini tanimlar.
// sistemdeki gorevi: Entegrasyon ve satir hata mesajlari ayni dosya adlarini okur.
public static class GtfsFileNames
{
    // Kaynak servisten indirilen veri setinin GTFS'te alisilmis arsiv adi.
    public const string Archive = "gtfs.zip";

    public const string Agency = "agency.txt";
    public const string Levels = "levels.txt";
    public const string Stops = "stops.txt";
    public const string Routes = "routes.txt";
    public const string Calendar = "calendar.txt";
    public const string CalendarDates = "calendar_dates.txt";
    public const string Shapes = "shapes.txt";
    public const string Trips = "trips.txt";
    public const string StopTimes = "stop_times.txt";
    public const string Frequencies = "frequencies.txt";
    public const string Transfers = "transfers.txt";
    public const string Pathways = "pathways.txt";
    public const string Areas = "areas.txt";
    public const string StopAreas = "stop_areas.txt";
    public const string Networks = "networks.txt";
    public const string RouteNetworks = "route_networks.txt";
    public const string LocationGroups = "location_groups.txt";
    public const string LocationGroupStops = "location_group_stops.txt";
    public const string BookingRules = "booking_rules.txt";
    public const string Timeframes = "timeframes.txt";
    public const string FareAttributes = "fare_attributes.txt";
    public const string FareRules = "fare_rules.txt";
    public const string RiderCategories = "rider_categories.txt";
    public const string FareMedia = "fare_media.txt";
    public const string FareProducts = "fare_products.txt";
    public const string FareLegRules = "fare_leg_rules.txt";
    public const string FareLegJoinRules = "fare_leg_join_rules.txt";
    public const string FareTransferRules = "fare_transfer_rules.txt";
    public const string FeedInfo = "feed_info.txt";
    public const string Attributions = "attributions.txt";
    public const string Translations = "translations.txt";
}
