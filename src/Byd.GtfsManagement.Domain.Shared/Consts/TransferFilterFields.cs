namespace Byd.GtfsManagement.Consts;

// islevi: Transfer liste filtrelerinin public alan adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusunun izinli filtre sozlesmesini sabitler.
/// <summary>GTFS aktarma filtre adlarini tanimlar.</summary>
public static class TransferFilterFields
{
    public const string FeedId = "feedId";
    public const string FromStopId = "fromStopId";
    public const string ToStopId = "toStopId";
    public const string FromRouteId = "fromRouteId";
    public const string ToRouteId = "toRouteId";
    public const string FromTripId = "fromTripId";
    public const string ToTripId = "toTripId";
    public const string TransferType = "transferType";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string FromStopId = "FromStopId";
        public const string ToStopId = "ToStopId";
        public const string FromRouteId = "FromRouteId";
        public const string ToRouteId = "ToRouteId";
        public const string FromTripId = "FromTripId";
        public const string ToTripId = "ToTripId";
        public const string TransferType = "TransferType";
        public const string CreationTime = "CreationTime";
    }
}
