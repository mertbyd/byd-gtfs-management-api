namespace Byd.GtfsManagement.Consts;

// islevi: BookingRule filtre adlarini tanimlar.
// sistemdeki gorevi: DataTable sorgusu icin izinli alanlari sabitler.
/// <summary>GTFS rezervasyon kurali filtre alanlarini tanimlar.</summary>
public static class BookingRuleFilterFields
{
    public const string FeedId = "feedId";
    public const string GtfsBookingRuleId = "gtfsBookingRuleId";
    public const string BookingType = "bookingType";
    public const string PriorNoticeDurationMin = "priorNoticeDurationMin";
    public const string PriorNoticeDurationMax = "priorNoticeDurationMax";
    public const string PriorNoticeLastDay = "priorNoticeLastDay";
    public const string PriorNoticeLastTime = "priorNoticeLastTime";
    public const string PriorNoticeStartDay = "priorNoticeStartDay";
    public const string PriorNoticeStartTime = "priorNoticeStartTime";
    public const string PriorNoticeServiceId = "priorNoticeServiceId";
    public const string Message = "message";
    public const string PickupMessage = "pickupMessage";
    public const string DropOffMessage = "dropOffMessage";
    public const string PhoneNumber = "phoneNumber";
    public const string InfoUrl = "infoUrl";
    public const string BookingUrl = "bookingUrl";
    public const string CreationTime = "creationTime";

    public static class PropertyPaths
    {
        public const string FeedId = "FeedId";
        public const string GtfsBookingRuleId = "GtfsBookingRuleId";
        public const string BookingType = "BookingType";
        public const string PriorNoticeDurationMin = "PriorNoticeDurationMin";
        public const string PriorNoticeDurationMax = "PriorNoticeDurationMax";
        public const string PriorNoticeLastDay = "PriorNoticeLastDay";
        public const string PriorNoticeLastTime = "PriorNoticeLastTime";
        public const string PriorNoticeStartDay = "PriorNoticeStartDay";
        public const string PriorNoticeStartTime = "PriorNoticeStartTime";
        public const string PriorNoticeServiceId = "PriorNoticeServiceId";
        public const string Message = "Message";
        public const string PickupMessage = "PickupMessage";
        public const string DropOffMessage = "DropOffMessage";
        public const string PhoneNumber = "PhoneNumber";
        public const string InfoUrl = "InfoUrl";
        public const string BookingUrl = "BookingUrl";
        public const string CreationTime = "CreationTime";
    }
}
