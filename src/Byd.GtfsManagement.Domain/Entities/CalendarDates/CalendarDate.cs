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
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // Istisnanin uygulandigi servisin kimligi.
    public Guid ServiceId { get; internal set; }

    // Istisnanin uygulandigi servis.
    public Service Service { get; internal set; } = default!;

    // GTFS exception_type lookup satirinin kimligi.
    public int ExceptionTypeId { get; internal set; }

    // Gunun servise eklendigini mi yoksa cikarildigini mi belirten istisna turu.
    public ExceptionType ExceptionType { get; internal set; } = default!;

    // GTFS date; istisnanin uygulandigi gun.
    public DateOnly Date { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected CalendarDate()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgisini kurar; duzenlenebilir alanlari Manager doldurur.
    public CalendarDate(Guid id, Guid? tenantId, Guid feedId) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
    }
}
