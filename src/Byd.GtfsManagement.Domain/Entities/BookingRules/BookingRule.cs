using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.BookingRules;

// islevi: booking_rules.txt icindeki GTFS verilerini saklar.
// sistemdeki gorevi: Rezervasyon kurali verilerini Feed altinda tutar.
/// <summary>GTFS rezervasyon kurali kaydini temsil eder.</summary>
public class BookingRule : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu tenant kimligi.
    public Guid? TenantId { get; internal set; }

    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; internal set; } = default!;

    // GTFS booking_rule_id dis kimligi.
    public string GtfsBookingRuleId { get; internal set; } = default!;

    // Onceden bildirim icin takvim servis kimligi.
    public string? PriorNoticeServiceId { get; internal set; }

    // Rezervasyon tipi (0: Gercek zamanli, 1: Ayni gun onceden, 2: Onceki gun(ler) onceden).
    public int BookingType { get; internal set; }

    // Minimum onceden bildirim suresi (dakika).
    public int? PriorNoticeDurationMin { get; internal set; }

    // Maksimum onceden bildirim suresi (dakika).
    public int? PriorNoticeDurationMax { get; internal set; }

    // Son rezervasyon gunu.
    public int? PriorNoticeLastDay { get; internal set; }

    // Son rezervasyon saati (HH:mm:ss).
    public string? PriorNoticeLastTime { get; internal set; }

    // Rezervasyon baslangic gunu.
    public int? PriorNoticeStartDay { get; internal set; }

    // Rezervasyon baslangic saati (HH:mm:ss).
    public string? PriorNoticeStartTime { get; internal set; }

    // Rezervasyon aciklama mesaji.
    public string? Message { get; internal set; }

    // Binis aciklama mesaji.
    public string? PickupMessage { get; internal set; }

    // Inis aciklama mesaji.
    public string? DropOffMessage { get; internal set; }

    // Rezervasyon telefon numarasi.
    public string? PhoneNumber { get; internal set; }

    // Bilgi web sayfasi adresi.
    public string? InfoUrl { get; internal set; }

    // Rezervasyon web sayfasi adresi.
    public string? BookingUrl { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected BookingRule()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public BookingRule(Guid id, Guid? tenantId, Guid feedId, string gtfsBookingRuleId, int bookingType) : base(id)
    {
        TenantId = tenantId;
        FeedId = feedId;
        GtfsBookingRuleId = gtfsBookingRuleId;
        BookingType = bookingType;
    }
}
