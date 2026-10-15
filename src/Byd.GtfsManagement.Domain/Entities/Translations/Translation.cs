using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Translations;

// islevi: translations.txt cok dilli ceviri alanlarini saklar.
// sistemdeki gorevi: Tablo ve alan bazli cevirileri Feed kapsaminda tutar.
/// <summary>GTFS ceviri kaydini temsil eder.</summary>
public class Translation : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Kayit bazli ceviride hedeflenen GTFS dis kayit kimligi.
    public string? RecordId { get; protected internal set; }

    // Bilesik anahtarlar icin alt kayit kimligi.
    public string? RecordSubId { get; protected internal set; }

    // Cevrilen GTFS tablosunun adi (orn: stops, routes).
    public string TableName { get; protected internal set; } = default!;

    // Cevrilen alanin adi (orn: stop_name).
    public string FieldName { get; protected internal set; } = default!;

    // IETF BCP 47 dil kodu (orn: en, tr).
    public string Language { get; protected internal set; } = default!;

    // Cevrilmis metin degeri.
    public string TranslationText { get; protected internal set; } = default!;

    // Deger bazli ceviride kaynak metin.
    public string? FieldValue { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Translation()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Translation(Guid id, Guid feedId, string tableName, string fieldName, string language, string translationText) : base(id)
    {
        FeedId = feedId;
        TableName = tableName;
        FieldName = fieldName;
        Language = language;
        TranslationText = translationText;
    }
}
