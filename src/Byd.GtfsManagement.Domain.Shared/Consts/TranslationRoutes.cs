namespace Byd.GtfsManagement.Consts;

// islevi: Translation HTTP rota sablonlarini tanimlar.
// sistemdeki gorevi: HTTP uclarini ve middleware harici tutma listesini esler.
/// <summary>GTFS ceviri rota yollarini tanimlar.</summary>
public static class TranslationRoutes
{
    public const string Root = "api/gtfs-management/translations";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
