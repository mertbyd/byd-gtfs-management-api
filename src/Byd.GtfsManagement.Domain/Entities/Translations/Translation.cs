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
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Kayit bazli ceviride hedeflenen GTFS dis kayit kimligi.
    public string? RecordId { get; internal set; }

    // Bilesik anahtarlar icin alt kayit kimligi.
    public string? RecordSubId { get; internal set; }

    // Cevrilen GTFS tablosunun adi (orn: stops, routes).
    public string TableName { get; internal set; } = default!;

    // Cevrilen alanin adi (orn: stop_name).
    public string FieldName { get; internal set; } = default!;

    // IETF BCP 47 dil kodu (orn: en, tr).
    public string Language { get; internal set; } = default!;

    // Cevrilmis metin degeri.
    public string TranslationText { get; internal set; } = default!;

    // Deger bazli ceviride kaynak metin.
    public string? FieldValue { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Translation()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public Translation(Guid id, Guid? tenantId, Guid feedId, string tableName, string fieldName, string language, string translationText) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        TableName = tableName;
        FieldName = fieldName;
        Language = language;
        TranslationText = translationText;
    }
}
