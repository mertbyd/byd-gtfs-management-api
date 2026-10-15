using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Translations;

// islevi: translations.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Translation ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Translations, GtfsImportOrders.Translation)]
public class TranslationImportDto
{
    [Name(GtfsColumnNames.RecordId)]
    public string? RecordId { get; set; }

    [Name(GtfsColumnNames.RecordSubId)]
    public string? RecordSubId { get; set; }

    [Name(GtfsColumnNames.TableName)]
    public string TableName { get; set; } = default!;

    [Name(GtfsColumnNames.FieldName)]
    public string FieldName { get; set; } = default!;

    [Name(GtfsColumnNames.Language)]
    public string Language { get; set; } = default!;

    [Name(GtfsColumnNames.Translation)]
    public string TranslationText { get; set; } = default!;

    [Name(GtfsColumnNames.FieldValue)]
    public string? FieldValue { get; set; }
}
