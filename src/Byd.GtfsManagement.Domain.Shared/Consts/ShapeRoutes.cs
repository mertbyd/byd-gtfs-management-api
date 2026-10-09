namespace Byd.GtfsManagement.Consts;

// islevi: Shape HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared uzerinden sunar.
/// <summary>GTFS sekil HTTP yollarini tanimlar.</summary>
public static class ShapeRoutes
{
    public const string Root = "api/gtfs-management/shapes";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
