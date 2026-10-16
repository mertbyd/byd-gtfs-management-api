namespace Byd.GtfsManagement.Consts;

// islevi: Feed HTTP yolunu tek noktada tanimlar.
// sistemdeki gorevi: Controller rota sozlesmesini sabit tutar.
public static class FeedRoutes
{
    public const string Root = "api/gtfs-management/feeds";
    public const string ById = "{id:guid}";
    public const string IncludingInactive = "{id:guid}/including-inactive";
    public const string ImportFromSource = "import-from-source";
    public const string FilteredList = "filtered-list";
    public const string Activate = "{id:guid}/activate";
    public const string Validate = "{id:guid}/validate";
    public const string ValidationReport = "{id:guid}/validation-report";
    public const string Summary = "{id:guid}/summary";
    public const string ExportGtfs = "{id:guid}/export";
}
