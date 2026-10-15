using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.Calendars;

// islevi: calendar.txt icindeki haftalik hizmet desenini ve gecerlilik araligini saklar.
// sistemdeki gorevi: Bir servisin hangi gunlerde ve hangi tarih araliginda calistigini tutar.
public class Calendar : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Haftalik desenin tanimladigi servisin kimligi.
    public Guid ServiceId { get; protected internal set; }

    // GTFS monday; servisin pazartesi calisip calismadigi.
    public bool Monday { get; protected internal set; }

    // GTFS tuesday; servisin sali calisip calismadigi.
    public bool Tuesday { get; protected internal set; }

    // GTFS wednesday; servisin carsamba calisip calismadigi.
    public bool Wednesday { get; protected internal set; }

    // GTFS thursday; servisin persembe calisip calismadigi.
    public bool Thursday { get; protected internal set; }

    // GTFS friday; servisin cuma calisip calismadigi.
    public bool Friday { get; protected internal set; }

    // GTFS saturday; servisin cumartesi calisip calismadigi.
    public bool Saturday { get; protected internal set; }

    // GTFS sunday; servisin pazar calisip calismadigi.
    public bool Sunday { get; protected internal set; }

    // GTFS start_date; desenin gecerli oldugu ilk gun.
    public DateOnly StartDate { get; protected internal set; }

    // GTFS end_date; desenin gecerli oldugu son gun.
    public DateOnly EndDate { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Desenin tanimladigi servis.
    public Service Service { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected Calendar()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public Calendar(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
