namespace Byd.GtfsManagement.Consts;

// islevi: StopTime filtre alanlarini ve repository property yollarini tanimlar.
// sistemdeki gorevi: Dinamik listeleme icin tipli izin listesini sabitler.
/// <summary>StopTime filtre alanlarini tanimlar.</summary>
public static class StopTimeFilterFields
{
    public const string FeedId = "feedId";
    public const string TripId = "tripId";
    public const string StopId = "stopId";
    public const string StopSequence = "stopSequence";
    public const string ArrivalTime = "arrivalTime";
    public const string DepartureTime = "departureTime";
    public const string CreationTime = "creationTime";

    // islevi: Public filtre adlarini entity alan adlarina baglar.
    // sistemdeki gorevi: DataTable sorgusunun izinli property yollarini sabitler.
    /// <summary>StopTime entity alan yollarini tanimlar.</summary>
    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string TripId = "TripId";
        public const string StopId = "StopId";
        public const string StopSequence = "StopSequence";
        public const string ArrivalTime = "ArrivalTime";
        public const string DepartureTime = "DepartureTime";
        public const string CreationTime = "CreationTime";
    }
}
