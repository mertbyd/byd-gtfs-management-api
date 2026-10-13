using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Services;

// islevi: trips.txt'in referans verdigi GTFS service_id kimligini saklar.
// sistemdeki gorevi: calendar.txt haftalik deseni ile calendar_dates.txt istisnalarini tek kimlik altinda toplar;
// GTFS her iki dosyanin da tek basina servis tanimlamasina izin verdigi icin kimlik ayri bir kayittir.
public class Service : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS service_id; veri seti icinde servisi tanimlayan kimlik.
    public string GtfsServiceId { get; internal set; } = default!;

    // Baglanan kayitlarin cevabinda gosterilen ad; GTFS servise ayri ad vermedigi icin service_id'dir. Kolon degildir.
    public string Name => GtfsServiceId;

    // EF materialization icin bos kabuk kurar.
    protected Service()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Service(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
