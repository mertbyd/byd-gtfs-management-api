namespace Byd.GtfsManagement;

// islevi: Modul hata kodlarinin kok ad alanini ve aile gruplarini tanimlar.
// sistemdeki gorevi: ABP hata mesajini Code.Split(':')[0] ile aradigi icin tek duz kok ad alanini garanti eder; aile bilgisi iki haneli grupta tasinir.
public static class GtfsManagementErrorCodes
{
    public const string RootNamespace = "GtfsManagement";

    public const string FeedGroup = RootNamespace + ":01";
    public const string AgencyGroup = RootNamespace + ":02";
    public const string StopGroup = RootNamespace + ":03";
    public const string RouteGroup = RootNamespace + ":04";
    public const string ServiceGroup = RootNamespace + ":05";
    public const string CalendarGroup = RootNamespace + ":06";
    public const string CalendarDateGroup = RootNamespace + ":07";
    public const string TripGroup = RootNamespace + ":08";
    public const string StopTimeGroup = RootNamespace + ":09";
    public const string FrequencyGroup = RootNamespace + ":10";
    public const string TransferGroup = RootNamespace + ":11";
    public const string ShapeGroup = RootNamespace + ":12";
    public const string FeedInfoGroup = RootNamespace + ":13";
    public const string LevelGroup = RootNamespace + ":14";
    public const string PathwayGroup = RootNamespace + ":15";
    public const string FareAttributeGroup = RootNamespace + ":16";
    public const string FareRuleGroup = RootNamespace + ":17";
    public const string TranslationGroup = RootNamespace + ":18";
    public const string AttributionGroup = RootNamespace + ":19";
    public const string AreaGroup = RootNamespace + ":20";
    public const string TimeframeGroup = RootNamespace + ":21";
    public const string RiderCategoryGroup = RootNamespace + ":22";
    public const string FareMediaGroup = RootNamespace + ":23";
    public const string FareProductGroup = RootNamespace + ":24";
    public const string FareLegRuleGroup = RootNamespace + ":25";
    public const string FareLegJoinRuleGroup = RootNamespace + ":26";
    public const string FareTransferRuleGroup = RootNamespace + ":27";
    public const string StopAreaGroup = RootNamespace + ":28";
    public const string NetworkGroup = RootNamespace + ":29";
    public const string RouteNetworkGroup = RootNamespace + ":30";
    public const string LocationGroupGroup = RootNamespace + ":31";
    public const string LocationGroupStopGroup = RootNamespace + ":32";
    public const string BookingRuleGroup = RootNamespace + ":33";
}
