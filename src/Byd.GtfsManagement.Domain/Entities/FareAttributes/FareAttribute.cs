using System;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareAttributes;

// islevi: fare_attributes.txt ucret sinifini, fiyat, para birimi ve transfer kurallarini saklar.
// sistemdeki gorevi: GTFS bilet ve ucret yapilandirmalarini Feed kapsaminda tutar.
/// <summary>GTFS ucret ozelligini temsil eder.</summary>
public class FareAttribute : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Istege bagli bagli ajans kimligi.
    public Guid? AgencyId { get; protected internal set; }

    // GTFS fare_id; ucret sinifini tanimlayan dis kimlik.
    public string GtfsFareId { get; protected internal set; } = default!;

    // Ucret tutari.
    public decimal Price { get; protected internal set; }

    // ISO 4217 para birimi kodu (orn: TRY, USD).
    public string CurrencyType { get; protected internal set; } = default!;

    // GTFS payment_method; odemenin yapilacagi an (0: arac ici, 1: binis oncesi).
    public int PaymentMethod { get; protected internal set; }

    // GTFS transfers; izin verilen aktarma adedi (0, 1, 2 veya sinirsiz icin null).
    public int? Transfers { get; protected internal set; }

    // Aktarmanin gecerlilik suresi (saniye).
    public int? TransferDuration { get; protected internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad; GTFS ucret sinifina ayri ad vermedigi icin fare_id'dir. Kolon degildir.
    public string Name => GtfsFareId;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Bagli ajans kaydi.
    public Agency? Agency { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected FareAttribute()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareAttribute(Guid id, Guid feedId, string gtfsFareId) : base(id)
    {
        FeedId = feedId;
        GtfsFareId = gtfsFareId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsFareId;
}
