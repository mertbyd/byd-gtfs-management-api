namespace Byd.GtfsManagement.Consts;

// islevi: Frequency HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared uzerinden sunar.
/// <summary>Sefer sikligi HTTP yollarini tanimlar.</summary>
public static class FrequencyRoutes
{
    public const string Root = "api/gtfs-management/frequencies";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
