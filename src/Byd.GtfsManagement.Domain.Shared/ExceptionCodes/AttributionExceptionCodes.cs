namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: attributions.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Atif hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS atif hata kodlarini tanimlar.</summary>
public static class AttributionExceptionCodes
{
    public const string OrganizationNameRequired = GtfsManagementErrorCodes.AttributionGroup + "0001";
    public const string OrganizationNameTooLong = GtfsManagementErrorCodes.AttributionGroup + "0002";
    public const string AttributionIdTooLong = GtfsManagementErrorCodes.AttributionGroup + "0003";
    public const string AttributionUrlTooLong = GtfsManagementErrorCodes.AttributionGroup + "0004";
    public const string AttributionEmailTooLong = GtfsManagementErrorCodes.AttributionGroup + "0005";
    public const string AttributionPhoneTooLong = GtfsManagementErrorCodes.AttributionGroup + "0006";
    public const string AttributionAlreadyExists = GtfsManagementErrorCodes.AttributionGroup + "0007";
    public const string AgencyOutOfFeed = GtfsManagementErrorCodes.AttributionGroup + "0009";
    public const string RouteOutOfFeed = GtfsManagementErrorCodes.AttributionGroup + "0010";
    public const string TripOutOfFeed = GtfsManagementErrorCodes.AttributionGroup + "0011";
    public const string InvalidFlagValue = GtfsManagementErrorCodes.AttributionGroup + "0012";
}
