using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Agencies;

// islevi: agency.txt icindeki ulasim markasini ve iletisim bilgilerini saklar.
// sistemdeki gorevi: GTFS ajansini Feed kapsaminda tutar.
public class Agency : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Istege bagli cemv_support lookup satirinin kimligi.
    public int? CemvSupportId { get; protected internal set; }

    // GTFS agency_id; tek ajansli veri setinde bos olabilir.
    public string? GtfsAgencyId { get; protected internal set; }

    // GTFS agency_name; yolcuya gosterilen tam ajans adi.
    public string Name { get; protected internal set; } = default!;

    // GTFS agency_url; ajansin web adresi.
    public string Url { get; protected internal set; } = default!;

    // GTFS agency_timezone; sefer saatlerinin yorumlandigi saat dilimi.
    public string Timezone { get; protected internal set; } = default!;

    // GTFS agency_lang; ajansin kullandigi birincil dil kodu.
    public string? Language { get; protected internal set; }

    // GTFS agency_phone; yolcular icin sesli iletisim numarasi.
    public string? Phone { get; protected internal set; }

    // GTFS agency_fare_url; bilet veya ucret bilgisi adresi.
    public string? FareUrl { get; protected internal set; }

    // GTFS agency_email; yolcu hizmetlerinin izledigi e-posta adresi.
    public string? Email { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Ajans genelindeki temassiz EMV odeme destegi aciklamasi.
    public CemvSupport? CemvSupport { get; protected internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Agency()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Agency(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsAgencyId;
}
