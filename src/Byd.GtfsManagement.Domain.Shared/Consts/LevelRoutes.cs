namespace Byd.GtfsManagement.Consts;

// islevi: Level HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared uzerinden sunar.
/// <summary>GTFS kat HTTP yollarini tanimlar.</summary>
public static class LevelRoutes
{
    public const string Root = "api/gtfs-management/levels";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
