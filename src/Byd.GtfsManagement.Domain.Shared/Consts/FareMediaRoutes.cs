namespace Byd.GtfsManagement.Consts;

// islevi: FareMedia HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ucret medyasi rota yollarini tanimlar.</summary>
public static class FareMediaRoutes
{
    public const string Root = "api/gtfs-management/fare-media";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
