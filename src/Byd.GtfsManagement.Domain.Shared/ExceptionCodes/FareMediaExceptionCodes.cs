namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_media.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret medyasi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret medyasi hata kodlarini tanimlar.</summary>
public static class FareMediaExceptionCodes
{
    public const string FareMediaAlreadyExists = GtfsManagementErrorCodes.FareMediaGroup + "0002";
    public const string GtfsFareMediaIdRequired = GtfsManagementErrorCodes.FareMediaGroup + "0003";
    public const string GtfsFareMediaIdTooLong = GtfsManagementErrorCodes.FareMediaGroup + "0004";
    public const string FareMediaNameTooLong = GtfsManagementErrorCodes.FareMediaGroup + "0005";
    public const string FareMediaTypeRequired = GtfsManagementErrorCodes.FareMediaGroup + "0006";
    public const string FareMediaTypeInvalid = GtfsManagementErrorCodes.FareMediaGroup + "0007";
}
