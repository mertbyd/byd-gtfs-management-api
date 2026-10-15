using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.BookingRules;

// islevi: booking_rules.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: BookingRule ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.BookingRules, GtfsImportOrders.BookingRule)]
public class BookingRuleImportDto
{
    // Foreign Keys
    [Name(GtfsColumnNames.PriorNoticeServiceId)]
    public string? PriorNoticeServiceId { get; set; }

    // Table Columns / Data
    [Name(GtfsColumnNames.BookingRuleId)]
    public string GtfsBookingRuleId { get; set; } = default!;
    [Name(GtfsColumnNames.BookingType)]
    public int BookingType { get; set; }
    [Name(GtfsColumnNames.PriorNoticeDurationMin)]
    public int? PriorNoticeDurationMin { get; set; }
    [Name(GtfsColumnNames.PriorNoticeDurationMax)]
    public int? PriorNoticeDurationMax { get; set; }
    [Name(GtfsColumnNames.PriorNoticeLastDay)]
    public int? PriorNoticeLastDay { get; set; }
    [Name(GtfsColumnNames.PriorNoticeLastTime)]
    public string? PriorNoticeLastTime { get; set; }
    [Name(GtfsColumnNames.PriorNoticeStartDay)]
    public int? PriorNoticeStartDay { get; set; }
    [Name(GtfsColumnNames.PriorNoticeStartTime)]
    public string? PriorNoticeStartTime { get; set; }
    [Name(GtfsColumnNames.Message)]
    public string? Message { get; set; }
    [Name(GtfsColumnNames.PickupMessage)]
    public string? PickupMessage { get; set; }
    [Name(GtfsColumnNames.DropOffMessage)]
    public string? DropOffMessage { get; set; }
    [Name(GtfsColumnNames.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    [Name(GtfsColumnNames.InfoUrl)]
    public string? InfoUrl { get; set; }
    [Name(GtfsColumnNames.BookingUrl)]
    public string? BookingUrl { get; set; }
}
