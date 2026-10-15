using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.BookingRules;

// islevi: booking_rules.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Rezervasyon kurali verilerini Feed altinda tutar.
/// <summary>GTFS rezervasyon kurali kaydini temsil eder.</summary>
public class BookingRule : AuditedAggregateRoot<Guid>, IGtfsEntity, IHasGtfsId
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // GTFS booking_rule_id dis kimligi.
    public string GtfsBookingRuleId { get; protected internal set; } = default!;

    // Onceden bildirim icin takvim servis kimligi.
    public string? PriorNoticeServiceId { get; protected internal set; }

    // Rezervasyon tipi (0: Gercek zamanli, 1: Ayni gun onceden, 2: Onceki gun(ler) onceden).
    public int BookingType { get; protected internal set; }

    // Minimum onceden bildirim suresi (dakika).
    public int? PriorNoticeDurationMin { get; protected internal set; }

    // Maksimum onceden bildirim suresi (dakika).
    public int? PriorNoticeDurationMax { get; protected internal set; }

    // Son rezervasyon gunu.
    public int? PriorNoticeLastDay { get; protected internal set; }

    // Son rezervasyon saati (HH:mm:ss).
    public string? PriorNoticeLastTime { get; protected internal set; }

    // Rezervasyon baslangic gunu.
    public int? PriorNoticeStartDay { get; protected internal set; }

    // Rezervasyon baslangic saati (HH:mm:ss).
    public string? PriorNoticeStartTime { get; protected internal set; }

    // Rezervasyon aciklama mesaji.
    public string? Message { get; protected internal set; }

    // Binis aciklama mesaji.
    public string? PickupMessage { get; protected internal set; }

    // Inis aciklama mesaji.
    public string? DropOffMessage { get; protected internal set; }

    // Rezervasyon telefon numarasi.
    public string? PhoneNumber { get; protected internal set; }

    // Bilgi web sayfasi adresi.
    public string? InfoUrl { get; protected internal set; }

    // Rezervasyon web sayfasi adresi.
    public string? BookingUrl { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected BookingRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public BookingRule(Guid id, Guid feedId, string gtfsBookingRuleId, int bookingType) : base(id)
    {
        FeedId = feedId;
        GtfsBookingRuleId = gtfsBookingRuleId;
        BookingType = bookingType;
    }
    // Dis anahtarlar disa aktarimda orijinal GTFS kimligine cozulur.
    string? IHasGtfsId.GtfsId => GtfsBookingRuleId;
}
