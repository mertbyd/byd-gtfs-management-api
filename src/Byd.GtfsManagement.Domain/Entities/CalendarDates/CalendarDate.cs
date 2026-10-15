using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.CalendarDates;

// islevi: calendar_dates.txt icindeki tek gunluk hizmet istisnasini saklar.
// sistemdeki gorevi: Haftalik desene eklenen veya desenden cikarilan gunleri tutar.
public class CalendarDate : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Istisnanin uygulandigi servisin kimligi.
    public Guid ServiceId { get; protected internal set; }

    // GTFS exception_type lookup satirinin kimligi.
    public int ExceptionTypeId { get; protected internal set; }

    // GTFS date; istisnanin uygulandigi gun.
    public DateOnly Date { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;
    // Istisnanin uygulandigi servis.
    public Service Service { get; protected internal set; } = default!;
    // Gunun servise eklendigini mi yoksa cikarildigini mi belirten istisna turu.
    public ExceptionType ExceptionType { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected CalendarDate()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public CalendarDate(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
