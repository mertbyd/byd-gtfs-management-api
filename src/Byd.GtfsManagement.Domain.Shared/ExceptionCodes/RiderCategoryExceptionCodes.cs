namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: rider_categories.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Yolcu kategorisi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS yolcu kategorisi hata kodlarini tanimlar.</summary>
public static class RiderCategoryExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.RiderCategoryGroup + "0001";
    public const string RiderCategoryAlreadyExists = GtfsManagementErrorCodes.RiderCategoryGroup + "0002";
    public const string GtfsRiderCategoryIdRequired = GtfsManagementErrorCodes.RiderCategoryGroup + "0003";
    public const string GtfsRiderCategoryIdTooLong = GtfsManagementErrorCodes.RiderCategoryGroup + "0004";
    public const string RiderCategoryNameRequired = GtfsManagementErrorCodes.RiderCategoryGroup + "0005";
    public const string RiderCategoryNameTooLong = GtfsManagementErrorCodes.RiderCategoryGroup + "0006";
    public const string EligibilityUrlTooLong = GtfsManagementErrorCodes.RiderCategoryGroup + "0007";
}
