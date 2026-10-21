namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: Feed yukleme, URL'den indirme, dogrulama, ice aktarim ve aktiflestirme hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: Kapsam, ad, yukleme girdisi, dis servis, arsiv satiri ve durum gecisi hatalarini lokalize edilebilir
// sozlesme yapar.
public static class FeedExceptionCodes
{
    public const string ScopeRequired = GtfsManagementErrorCodes.FeedGroup + "0001";
    public const string NameRequired = GtfsManagementErrorCodes.FeedGroup + "0002";
    public const string NameTooLong = GtfsManagementErrorCodes.FeedGroup + "0003";
    public const string FileNameRequired = GtfsManagementErrorCodes.FeedGroup + "0004";
    public const string FileNameTooLong = GtfsManagementErrorCodes.FeedGroup + "0005";
    public const string ArchiveRequired = GtfsManagementErrorCodes.FeedGroup + "0006";
    public const string DataRequired = GtfsManagementErrorCodes.FeedGroup + "0007";
    public const string SourceNotConfigured = GtfsManagementErrorCodes.FeedGroup + "0008";
    public const string SourceUnavailable = GtfsManagementErrorCodes.FeedGroup + "0009";
    public const string ValidatorNotConfigured = GtfsManagementErrorCodes.FeedGroup + "0010";
    public const string ValidatorUnavailable = GtfsManagementErrorCodes.FeedGroup + "0011";
    public const string ValidatorResponseInvalid = GtfsManagementErrorCodes.FeedGroup + "0012";
    public const string StatusTransitionInvalid = GtfsManagementErrorCodes.FeedGroup + "0013";
    public const string RecordInvalid = GtfsManagementErrorCodes.FeedGroup + "0014";
    public const string LookupCodeUnknown = GtfsManagementErrorCodes.FeedGroup + "0015";
    public const string RecordRejected = GtfsManagementErrorCodes.FeedGroup + "0016";
    public const string ValidationReportMissing = GtfsManagementErrorCodes.FeedGroup + "0017";
    public const string NotIntegrated = GtfsManagementErrorCodes.FeedGroup + "0018";
    public const string AlreadyActive = GtfsManagementErrorCodes.FeedGroup + "0019";
    public const string ValidationErrorsPresent = GtfsManagementErrorCodes.FeedGroup + "0020";
    public const string InactiveFeedNotEditable = GtfsManagementErrorCodes.FeedGroup + "0021";
    public const string FeedRequired = GtfsManagementErrorCodes.FeedGroup + "0022";
    public const string IntegrationUnavailable = GtfsManagementErrorCodes.FeedGroup + "0024";
    public const string ExportUnavailable = GtfsManagementErrorCodes.FeedGroup + "0025";
    public const string NameInvalid = GtfsManagementErrorCodes.FeedGroup + "0026";
    public const string SourceUrlInvalid = GtfsManagementErrorCodes.FeedGroup + "0027";
    public const string ActiveFeedRequired = GtfsManagementErrorCodes.FeedGroup + "0028";
    public const string ChangeRejectedByValidation = GtfsManagementErrorCodes.FeedGroup + "0029";
}
