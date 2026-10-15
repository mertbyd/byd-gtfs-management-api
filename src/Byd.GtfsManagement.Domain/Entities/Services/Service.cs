using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Services;

// islevi: trips.txt'in referans verdigi GTFS service_id kimligini saklar.
// sistemdeki gorevi: calendar.txt haftalik deseni ile calendar_dates.txt istisnalarini tek kimlik altinda toplar;
// GTFS her iki dosyanin da tek basina servis tanimlamasina izin verdigi icin kimlik ayri bir kayittir.
public class Service : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS service_id; veri seti icinde servisi tanimlayan kimlik.
    public string GtfsServiceId { get; protected internal set; } = default!;

    // Baglanan kayitlarin cevabinda gosterilen ad; GTFS servise ayri ad vermedigi icin service_id'dir. Kolon degildir.
    public string Name => GtfsServiceId;

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Service()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Service(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }

    // Disa aktarimin bu kayda bakan yabanci anahtarlari cevirdigi GTFS kimligini public setter acmadan verir.
    string? IHasGtfsId.GtfsId => GtfsServiceId;
}
