namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_attributes.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret ozelligi hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret ozelligi hata kodlarini tanimlar.</summary>
public static class FareAttributeExceptionCodes
{
    public const string FareRequired = GtfsManagementErrorCodes.FareAttributeGroup + "0001";
    public const string FareTooLong = GtfsManagementErrorCodes.FareAttributeGroup + "0002";
    public const string PriceInvalid = GtfsManagementErrorCodes.FareAttributeGroup + "0003";
    public const string CurrencyTypeRequired = GtfsManagementErrorCodes.FareAttributeGroup + "0004";
    public const string CurrencyTypeInvalid = GtfsManagementErrorCodes.FareAttributeGroup + "0005";
    public const string PaymentMethodInvalid = GtfsManagementErrorCodes.FareAttributeGroup + "0006";
    public const string TransfersInvalid = GtfsManagementErrorCodes.FareAttributeGroup + "0007";
    public const string TransferDurationInvalid = GtfsManagementErrorCodes.FareAttributeGroup + "0008";
    public const string FareAlreadyExists = GtfsManagementErrorCodes.FareAttributeGroup + "0009";
    public const string FeedRequired = GtfsManagementErrorCodes.FareAttributeGroup + "0010";
    public const string AgencyOutOfFeed = GtfsManagementErrorCodes.FareAttributeGroup + "0011";
}
