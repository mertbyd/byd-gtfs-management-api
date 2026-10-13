namespace Byd.GtfsManagement.ExceptionCodes;

// islevi: fare_rules.txt dogrulama ve is kurali hata kodlarini tanimlar.
// sistemdeki gorevi: Ucret kurali hatalarini lokalize edilebilir GTFS hata ailesine baglar.
/// <summary>GTFS ucret kurali hata kodlarini tanimlar.</summary>
public static class FareRuleExceptionCodes
{
    public const string FareRequired = GtfsManagementErrorCodes.FareRuleGroup + "0001";
    public const string FareOutOfFeed = GtfsManagementErrorCodes.FareRuleGroup + "0002";
    public const string RouteOutOfFeed = GtfsManagementErrorCodes.FareRuleGroup + "0003";
    public const string OriginIdTooLong = GtfsManagementErrorCodes.FareRuleGroup + "0004";
    public const string DestinationIdTooLong = GtfsManagementErrorCodes.FareRuleGroup + "0005";
    public const string ContainsIdTooLong = GtfsManagementErrorCodes.FareRuleGroup + "0006";
    public const string RuleAlreadyExists = GtfsManagementErrorCodes.FareRuleGroup + "0007";
    public const string FeedRequired = GtfsManagementErrorCodes.FareRuleGroup + "0008";
}
