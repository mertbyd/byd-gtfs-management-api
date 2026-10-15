namespace Byd.GtfsManagement.Consts;

// islevi: Pathway HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared uzerinden sunar.
/// <summary>GTFS yuruyus yolu HTTP yollarini tanimlar.</summary>
public static class PathwayRoutes
{
    public const string Root = "api/gtfs-management/pathways";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
