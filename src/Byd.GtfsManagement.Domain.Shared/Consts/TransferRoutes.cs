namespace Byd.GtfsManagement.Consts;

// islevi: Transfer HTTP yollarini tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini Domain.Shared katmaninda tutar.
/// <summary>GTFS aktarma HTTP yollarini tanimlar.</summary>
public static class TransferRoutes
{
    public const string Root = "api/gtfs-management/transfers";
    public const string ById = "{id:guid}";
    public const string FilteredList = "filtered-list";
}
