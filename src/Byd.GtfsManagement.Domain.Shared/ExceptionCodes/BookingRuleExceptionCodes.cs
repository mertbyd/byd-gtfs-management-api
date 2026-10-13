namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: booking_rules.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Rezervasyon kurali hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS rezervasyon kurali hata kodlarini tanimlar.</summary>
public static class BookingRuleExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.BookingRuleGroup + "0001";
    public const string BookingRuleAlreadyExists = GtfsManagementErrorCodes.BookingRuleGroup + "0002";
    public const string GtfsBookingRuleIdRequired = GtfsManagementErrorCodes.BookingRuleGroup + "0003";
    public const string GtfsBookingRuleIdTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0004";
    public const string BookingTypeRequired = GtfsManagementErrorCodes.BookingRuleGroup + "0005";
    public const string BookingTypeInvalid = GtfsManagementErrorCodes.BookingRuleGroup + "0006";
    public const string PriorNoticeLastTimeTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0007";
    public const string PriorNoticeStartTimeTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0008";
    public const string PriorNoticeServiceIdTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0009";
    public const string MessageTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0010";
    public const string PickupMessageTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0011";
    public const string DropOffMessageTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0012";
    public const string PhoneNumberTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0013";
    public const string InfoUrlTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0014";
    public const string BookingUrlTooLong = GtfsManagementErrorCodes.BookingRuleGroup + "0015";
}
