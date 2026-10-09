namespace Byd.GtfsManagement.Dtos.BookingRules;

// islevi: BookingRule duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Create ve Update isteklerine ortak public sozlesme sunar.
/// <summary>GTFS rezervasyon kurali ortak istek alanlarini tasir.</summary>
public abstract class BookingRuleInputDto
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
