namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: agency.txt alan ve is kurali hatalarinin kodlarini tanimlar.
// sistemdeki gorevi: GTFS ajans sozlesmesinin ihlallerini lokalize edilebilir kodlara baglar.
public static class AgencyExceptionCodes
{
    public const string GtfsAgencyIdTooLong = GtfsManagementErrorCodes.AgencyGroup + "0002";
    public const string NameRequired = GtfsManagementErrorCodes.AgencyGroup + "0003";
    public const string NameTooLong = GtfsManagementErrorCodes.AgencyGroup + "0004";
    public const string UrlRequired = GtfsManagementErrorCodes.AgencyGroup + "0005";
    public const string UrlTooLong = GtfsManagementErrorCodes.AgencyGroup + "0006";
    public const string TimezoneRequired = GtfsManagementErrorCodes.AgencyGroup + "0007";
    public const string TimezoneTooLong = GtfsManagementErrorCodes.AgencyGroup + "0008";
    public const string TimezoneInvalid = GtfsManagementErrorCodes.AgencyGroup + "0009";
    public const string LanguageTooLong = GtfsManagementErrorCodes.AgencyGroup + "0010";
    public const string PhoneTooLong = GtfsManagementErrorCodes.AgencyGroup + "0011";
    public const string FareUrlTooLong = GtfsManagementErrorCodes.AgencyGroup + "0012";
    public const string EmailTooLong = GtfsManagementErrorCodes.AgencyGroup + "0013";
    public const string EmailInvalid = GtfsManagementErrorCodes.AgencyGroup + "0014";
}
