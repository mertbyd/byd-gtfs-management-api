namespace Byd.GtfsManagement.Consts;

// islevi: CalendarDate filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class CalendarDateFilterFields
{
    public const string FeedId = "feedId";
    public const string ServiceId = "serviceId";
    public const string ExceptionTypeId = "exceptionTypeId";
    public const string Date = "date";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string ServiceId = "ServiceId";
        public const string ExceptionTypeId = "ExceptionTypeId";
        public const string Date = "Date";
        public const string CreationTime = "CreationTime";
    }
}
