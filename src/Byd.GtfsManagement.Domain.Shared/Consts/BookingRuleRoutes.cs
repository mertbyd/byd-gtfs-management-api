namespace Byd.GtfsManagement.Consts;

// islevi: BookingRule HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS rezervasyon kurali rota yollarini tanimlar.</summary>
public static class BookingRuleRoutes
{
    public const string Root = "api/gtfs-management/booking-rules";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
