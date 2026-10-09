using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Agencies;

// islevi: agency.txt icindeki ulasim markasini ve iletisim bilgilerini saklar.
// sistemdeki gorevi: GTFS ajansini tenant ve Feed kapsaminda tutar.
public class Agency : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Istege bagli cemv_support lookup satirinin kimligi.
    public int? CemvSupportId { get; internal set; }

    // Ajans genelindeki temassiz EMV odeme destegi aciklamasi.
    public CemvSupport? CemvSupport { get; internal set; }

    // GTFS agency_id; tek ajansli veri setinde bos olabilir.
    public string? GtfsAgencyId { get; internal set; }

    // GTFS agency_name; yolcuya gosterilen tam ajans adi.
    public string Name { get; internal set; } = default!;

    // GTFS agency_url; ajansin web adresi.
    public string Url { get; internal set; } = default!;

    // GTFS agency_timezone; sefer saatlerinin yorumlandigi saat dilimi.
    public string Timezone { get; internal set; } = default!;

    // GTFS agency_lang; ajansin kullandigi birincil dil kodu.
    public string? Language { get; internal set; }

    // GTFS agency_phone; yolcular icin sesli iletisim numarasi.
    public string? Phone { get; internal set; }

    // GTFS agency_fare_url; bilet veya ucret bilgisi adresi.
    public string? FareUrl { get; internal set; }

    // GTFS agency_email; yolcu hizmetlerinin izledigi e-posta adresi.
    public string? Email { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Agency()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Agency(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
