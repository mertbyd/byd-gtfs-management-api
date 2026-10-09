namespace Byd.GtfsManagement.Consts;

// islevi: Calendar filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class CalendarFilterFields
{
    public const string FeedId = "feedId";
    public const string ServiceId = "serviceId";
    public const string StartDate = "startDate";
    public const string EndDate = "endDate";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string ServiceId = "ServiceId";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string CreationTime = "CreationTime";
    }
}
