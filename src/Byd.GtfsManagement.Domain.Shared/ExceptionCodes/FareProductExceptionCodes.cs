namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_products.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret urunu hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret urunu hata kodlarini tanimlar.</summary>
public static class FareProductExceptionCodes
{
    public const string FareProductAlreadyExists = GtfsManagementErrorCodes.FareProductGroup + "0002";
    public const string GtfsFareProductIdRequired = GtfsManagementErrorCodes.FareProductGroup + "0003";
    public const string GtfsFareProductIdTooLong = GtfsManagementErrorCodes.FareProductGroup + "0004";
    public const string FareProductNameTooLong = GtfsManagementErrorCodes.FareProductGroup + "0005";
    public const string GtfsRiderCategoryIdTooLong = GtfsManagementErrorCodes.FareProductGroup + "0006";
    public const string GtfsFareMediaIdTooLong = GtfsManagementErrorCodes.FareProductGroup + "0007";
    public const string AmountRequired = GtfsManagementErrorCodes.FareProductGroup + "0008";
    public const string AmountInvalid = GtfsManagementErrorCodes.FareProductGroup + "0009";
    public const string CurrencyRequired = GtfsManagementErrorCodes.FareProductGroup + "0010";
    public const string CurrencyTooLong = GtfsManagementErrorCodes.FareProductGroup + "0011";
}
