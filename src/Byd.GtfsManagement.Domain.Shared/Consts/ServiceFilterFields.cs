namespace Byd.GtfsManagement.Consts;

// islevi: Service filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class ServiceFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsServiceId = "gtfsServiceId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsServiceId = "GtfsServiceId";
        public const string CreationTime = "CreationTime";
    }
}
