namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_transfer_rules.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret aktarma kurali hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret aktarma kurali hata kodlarini tanimlar.</summary>
public static class FareTransferRuleExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.FareTransferRuleGroup + "0001";
    public const string FareTransferRuleAlreadyExists = GtfsManagementErrorCodes.FareTransferRuleGroup + "0002";
    public const string FromLegGroupIdTooLong = GtfsManagementErrorCodes.FareTransferRuleGroup + "0003";
    public const string ToLegGroupIdTooLong = GtfsManagementErrorCodes.FareTransferRuleGroup + "0004";
    public const string FareTransferTypeRequired = GtfsManagementErrorCodes.FareTransferRuleGroup + "0005";
    public const string FareTransferTypeInvalid = GtfsManagementErrorCodes.FareTransferRuleGroup + "0006";
    public const string GtfsFareProductIdTooLong = GtfsManagementErrorCodes.FareTransferRuleGroup + "0007";
}
