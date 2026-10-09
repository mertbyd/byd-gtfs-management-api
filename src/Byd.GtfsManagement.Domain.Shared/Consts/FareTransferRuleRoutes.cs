namespace Byd.GtfsManagement.Consts;

// islevi: FareTransferRule HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret aktarma kurali rota yollarini tanimlar.</summary>
public static class FareTransferRuleRoutes
{
    public const string Root = "api/gtfs-management/fare-transfer-rules";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
