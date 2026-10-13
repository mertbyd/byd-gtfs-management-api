namespace Byd.GtfsManagement.Models.BookingRules;

// islevi: BookingRule olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS rezervasyon kurali ortak domain girdi modelini tasir.</summary>
public abstract class BookingRuleInputModel
{
    public string GtfsBookingRuleId { get; set; } = default!;
    public string? PriorNoticeServiceId { get; set; }
    public int BookingType { get; set; }
    public int? PriorNoticeDurationMin { get; set; }
    public int? PriorNoticeDurationMax { get; set; }
    public int? PriorNoticeLastDay { get; set; }
    public string? PriorNoticeLastTime { get; set; }
    public int? PriorNoticeStartDay { get; set; }
    public string? PriorNoticeStartTime { get; set; }
    public string? Message { get; set; }
    public string? PickupMessage { get; set; }
    public string? DropOffMessage { get; set; }
    public string? PhoneNumber { get; set; }
    public string? InfoUrl { get; set; }
    public string? BookingUrl { get; set; }
}
