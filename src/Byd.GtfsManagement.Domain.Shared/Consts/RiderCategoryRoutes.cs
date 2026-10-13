namespace Byd.GtfsManagement.Consts;

// islevi: RiderCategory HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS yolcu kategorisi rota yollarini tanimlar.</summary>
public static class RiderCategoryRoutes
{
    public const string Root = "api/gtfs-management/rider-categories";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
