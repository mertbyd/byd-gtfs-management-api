namespace Byd.GtfsManagement.Consts;

// islevi: Stop filtre yuzeyinde disariya acilan alan adlarini tanimlar.
// sistemdeki gorevi: Filtre DTO'su ile repository sorgu motoru arasindaki kararli alan adi sozlesmesidir.
public static class StopFilterFields
{
    public const string FeedId = "feedId";
    public const string LocationTypeId = "locationTypeId";
    public const string WheelchairBoardingId = "wheelchairBoardingId";
    public const string ParentStationId = "parentStationId";
    public const string GtfsStopId = "gtfsStopId";
    public const string Code = "code";
    public const string Name = "name";
    public const string ZoneId = "zoneId";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string LocationTypeId = "LocationTypeId";
        public const string WheelchairBoardingId = "WheelchairBoardingId";
        public const string ParentStationId = "ParentStationId";
        public const string GtfsStopId = "GtfsStopId";
        public const string Code = "Code";
        public const string Name = "Name";
        public const string ZoneId = "ZoneId";
        public const string CreationTime = "CreationTime";
    }
}
