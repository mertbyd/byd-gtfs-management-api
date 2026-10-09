namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_leg_join_rules.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret etap birlestirme kurali hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret etap birlestirme kurali hata kodlarini tanimlar.</summary>
public static class FareLegJoinRuleExceptionCodes
{
    public const string FeedRequired = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0001";
    public const string FareLegJoinRuleAlreadyExists = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0002";
    public const string FromNetworkIdTooLong = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0003";
    public const string ToNetworkIdTooLong = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0004";
    public const string FromStopIdTooLong = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0005";
    public const string ToStopIdTooLong = GtfsManagementErrorCodes.FareLegJoinRuleGroup + "0006";
}
