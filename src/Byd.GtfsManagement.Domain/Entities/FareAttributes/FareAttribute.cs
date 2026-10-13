using System;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FareAttributes;

// islevi: fare_attributes.txt ucret sinifini, fiyat, para birimi ve transfer kurallarini saklar.
// sistemdeki gorevi: GTFS bilet ve ucret yapilandirmalarini Feed kapsaminda tutar.
/// <summary>GTFS ucret ozelligini temsil eder.</summary>
public class FareAttribute : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS fare_id; ucret sinifini tanimlayan dis kimlik.
    public string GtfsFareId { get; internal set; } = default!;

    // Istege bagli bagli ajans kimligi.
    public Guid? AgencyId { get; internal set; }

    // Bagli ajans kaydi.
    public Agency? Agency { get; internal set; }

    // Ucret tutari.
    public decimal Price { get; internal set; }

    // ISO 4217 para birimi kodu (orn: TRY, USD).
    public string CurrencyType { get; internal set; } = default!;

    // GTFS payment_method; odemenin yapilacagi an (0: arac ici, 1: binis oncesi).
    public int PaymentMethod { get; internal set; }

    // GTFS transfers; izin verilen aktarma adedi (0, 1, 2 veya sinirsiz icin null).
    public int? Transfers { get; internal set; }

    // Aktarmanin gecerlilik suresi (saniye).
    public int? TransferDuration { get; internal set; }

    // Baglanan kayitlarin cevabinda gosterilen ad; GTFS ucret sinifina ayri ad vermedigi icin fare_id'dir. Kolon degildir.
    public string Name => GtfsFareId;

    // EF materialization icin bos kabuk kurar.
    protected FareAttribute()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FareAttribute(Guid id, Guid? tenantId, Guid feedId, string gtfsFareId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsFareId = gtfsFareId;
    }
}
