namespace Byd.GtfsManagement.Consts;

// islevi: booking_rules.txt tablo adi, indeks ve metin sinirlarini tanimlar.
// sistemdeki gorevi: GTFS rezervasyon kurali sozlesmesini Domain.Shared katmaninda sabitler.
/// <summary>GTFS rezervasyon kurali sabitlerini tanimlar.</summary>
public static class BookingRuleConsts
{
    public const string TableName = "BookingRules";
    public const string KeyIndex = "IX_BookingRules_FeedId_GtfsBookingRuleId";
    public const int MaxGtfsBookingRuleIdLength = 256;
    public const int MaxPriorNoticeLastTimeLength = 10;
    public const int MaxPriorNoticeStartTimeLength = 10;
    public const int MaxPriorNoticeServiceIdLength = 256;
    public const int MaxMessageLength = 2048;
    public const int MaxPickupMessageLength = 2048;
    public const int MaxDropOffMessageLength = 2048;
    public const int MaxPhoneNumberLength = 64;
    public const int MaxInfoUrlLength = 2048;
    public const int MaxBookingUrlLength = 2048;
}
