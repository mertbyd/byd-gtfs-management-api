using System;

namespace Byd.GtfsManagement.Dtos.Translations;

// islevi: translations.txt satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; RecordId ve RecordSubId dis GTFS kayit kimlikleridir.
/// <summary>GTFS ceviri cevap alanlarini tasir.</summary>
public class TranslationDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public string? RecordId { get; set; }
    public string? RecordSubId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string TableName { get; set; } = default!;
    public string FieldName { get; set; } = default!;
    public string Language { get; set; } = default!;
    public string TranslationText { get; set; } = default!;
    public string? FieldValue { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
