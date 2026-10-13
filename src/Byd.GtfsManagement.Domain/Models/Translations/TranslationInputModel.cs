namespace Byd.GtfsManagement.Models.Translations;

// islevi: Translation olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ceviri ortak domain girdi modelini tasir.</summary>
public abstract class TranslationInputModel
{
    public string TableName { get; set; } = default!;
    public string FieldName { get; set; } = default!;
    public string Language { get; set; } = default!;
    public string TranslationText { get; set; } = default!;
    public string? RecordId { get; set; }
    public string? RecordSubId { get; set; }
    public string? FieldValue { get; set; }
}
