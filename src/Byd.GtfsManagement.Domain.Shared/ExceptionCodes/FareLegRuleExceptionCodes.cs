namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_leg_rules.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret etap kurali hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret etap kurali hata kodlarini tanimlar.</summary>
public static class FareLegRuleExceptionCodes
{
    public const string FareLegRuleAlreadyExists = GtfsManagementErrorCodes.FareLegRuleGroup + "0002";
    public const string LegGroupIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0003";
    public const string NetworkIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0004";
    public const string FromAreaIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0005";
    public const string ToAreaIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0006";
    public const string FromTimeframeGroupIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0007";
    public const string ToTimeframeGroupIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0008";
    public const string GtfsFareProductIdRequired = GtfsManagementErrorCodes.FareLegRuleGroup + "0009";
    public const string GtfsFareProductIdTooLong = GtfsManagementErrorCodes.FareLegRuleGroup + "0010";
}
