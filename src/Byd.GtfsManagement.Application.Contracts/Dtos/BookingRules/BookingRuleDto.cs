using System;

namespace Byd.GtfsManagement.Dtos.BookingRules;

// islevi: BookingRule satirini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
/// <summary>GTFS rezervasyon kurali cevap alanlarini tasir.</summary>
public class BookingRuleDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
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
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
