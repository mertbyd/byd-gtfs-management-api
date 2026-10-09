namespace Byd.GtfsManagement.Dtos.Translations;

// islevi: translations.txt duzenlenebilir ceviri alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS ceviri ortak istek alanlarini tasir.</summary>
public abstract class TranslationInputDto
{
    public string? RecordId { get; set; }
    public string? RecordSubId { get; set; }
    public string TableName { get; set; } = default!;
    public string FieldName { get; set; } = default!;
    public string Language { get; set; } = default!;
    public string TranslationText { get; set; } = default!;
    public string? FieldValue { get; set; }
}
