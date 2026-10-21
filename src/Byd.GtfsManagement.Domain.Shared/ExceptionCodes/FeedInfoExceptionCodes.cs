namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: feed_info.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Yayimci hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS yayimci bilgisi hata kodlarini tanimlar.</summary>
public static class FeedInfoExceptionCodes
{
    public const string PublisherNameRequired = GtfsManagementErrorCodes.FeedInfoGroup + "0001";
    public const string PublisherNameTooLong = GtfsManagementErrorCodes.FeedInfoGroup + "0002";
    public const string PublisherUrlRequired = GtfsManagementErrorCodes.FeedInfoGroup + "0003";
    public const string PublisherUrlInvalid = GtfsManagementErrorCodes.FeedInfoGroup + "0004";
    public const string LanguageRequired = GtfsManagementErrorCodes.FeedInfoGroup + "0005";
    public const string LanguageTooLong = GtfsManagementErrorCodes.FeedInfoGroup + "0006";
    public const string DateOrderInvalid = GtfsManagementErrorCodes.FeedInfoGroup + "0007";
    public const string ContactEmailInvalid = GtfsManagementErrorCodes.FeedInfoGroup + "0008";
    public const string ContactUrlInvalid = GtfsManagementErrorCodes.FeedInfoGroup + "0009";
    public const string FeedInfoAlreadyExists = GtfsManagementErrorCodes.FeedInfoGroup + "0010";
    public const string DateFormatInvalid = GtfsManagementErrorCodes.FeedInfoGroup + "0012";
}
