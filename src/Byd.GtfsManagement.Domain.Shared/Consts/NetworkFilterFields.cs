namespace Byd.GtfsManagement.Consts;

// islevi: Network filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS ulasim agi filtre alanlarini tanimlar.</summary>
public static class NetworkFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsNetworkId = "gtfsNetworkId";
    public const string NetworkName = "networkName";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsNetworkId = "GtfsNetworkId";
        public const string NetworkName = "NetworkName";
        public const string CreationTime = "CreationTime";
    }
}
