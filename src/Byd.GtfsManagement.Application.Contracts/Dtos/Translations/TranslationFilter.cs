using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Translations;

// islevi: Translation liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Ceviri sorgusunun izinli alanlarini belirler.
/// <summary>GTFS ceviri filtrelerini tasir.</summary>
public class TranslationFilter : IFeedFilter
{
    [DataTable(TranslationFilterFields.FeedId, TranslationFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(TranslationFilterFields.TableName, TranslationFilterFields.PropertyPaths.TableName)]
    public DataTableFilterItem? TableName { get; set; }

    [DataTable(TranslationFilterFields.FieldName, TranslationFilterFields.PropertyPaths.FieldName)]
    public DataTableFilterItem? FieldName { get; set; }

    [DataTable(TranslationFilterFields.Language, TranslationFilterFields.PropertyPaths.Language)]
    public DataTableFilterItem? Language { get; set; }

    [DataTable(TranslationFilterFields.RecordId, TranslationFilterFields.PropertyPaths.RecordId)]
    public DataTableFilterItem? RecordId { get; set; }

    [DataTable(TranslationFilterFields.RecordSubId, TranslationFilterFields.PropertyPaths.RecordSubId)]
    public DataTableFilterItem? RecordSubId { get; set; }

    [DataTable(TranslationFilterFields.FieldValue, TranslationFilterFields.PropertyPaths.FieldValue)]
    public DataTableFilterItem? FieldValue { get; set; }

    [DataTable(TranslationFilterFields.TranslationText, TranslationFilterFields.PropertyPaths.TranslationText)]
    public DataTableFilterItem? TranslationText { get; set; }

    [DataTable(TranslationFilterFields.CreationTime, TranslationFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
