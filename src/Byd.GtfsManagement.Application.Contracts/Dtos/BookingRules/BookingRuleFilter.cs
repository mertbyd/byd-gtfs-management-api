using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.BookingRules;

// islevi: BookingRule liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Sorgunun izinli alanlarini belirler.
/// <summary>GTFS rezervasyon kurali filtrelerini tasir.</summary>
public class BookingRuleFilter
{
    [DataTable(BookingRuleFilterFields.FeedId, BookingRuleFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(BookingRuleFilterFields.GtfsBookingRuleId, BookingRuleFilterFields.PropertyPaths.GtfsBookingRuleId)]
    public DataTableFilterItem? GtfsBookingRuleId { get; set; }

    [DataTable(BookingRuleFilterFields.BookingType, BookingRuleFilterFields.PropertyPaths.BookingType)]
    public DataTableFilterItem? BookingType { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeDurationMin, BookingRuleFilterFields.PropertyPaths.PriorNoticeDurationMin)]
    public DataTableFilterItem? PriorNoticeDurationMin { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeDurationMax, BookingRuleFilterFields.PropertyPaths.PriorNoticeDurationMax)]
    public DataTableFilterItem? PriorNoticeDurationMax { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeLastDay, BookingRuleFilterFields.PropertyPaths.PriorNoticeLastDay)]
    public DataTableFilterItem? PriorNoticeLastDay { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeLastTime, BookingRuleFilterFields.PropertyPaths.PriorNoticeLastTime)]
    public DataTableFilterItem? PriorNoticeLastTime { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeStartDay, BookingRuleFilterFields.PropertyPaths.PriorNoticeStartDay)]
    public DataTableFilterItem? PriorNoticeStartDay { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeStartTime, BookingRuleFilterFields.PropertyPaths.PriorNoticeStartTime)]
    public DataTableFilterItem? PriorNoticeStartTime { get; set; }

    [DataTable(BookingRuleFilterFields.PriorNoticeServiceId, BookingRuleFilterFields.PropertyPaths.PriorNoticeServiceId)]
    public DataTableFilterItem? PriorNoticeServiceId { get; set; }

    [DataTable(BookingRuleFilterFields.Message, BookingRuleFilterFields.PropertyPaths.Message)]
    public DataTableFilterItem? Message { get; set; }

    [DataTable(BookingRuleFilterFields.PickupMessage, BookingRuleFilterFields.PropertyPaths.PickupMessage)]
    public DataTableFilterItem? PickupMessage { get; set; }

    [DataTable(BookingRuleFilterFields.DropOffMessage, BookingRuleFilterFields.PropertyPaths.DropOffMessage)]
    public DataTableFilterItem? DropOffMessage { get; set; }

    [DataTable(BookingRuleFilterFields.PhoneNumber, BookingRuleFilterFields.PropertyPaths.PhoneNumber)]
    public DataTableFilterItem? PhoneNumber { get; set; }

    [DataTable(BookingRuleFilterFields.InfoUrl, BookingRuleFilterFields.PropertyPaths.InfoUrl)]
    public DataTableFilterItem? InfoUrl { get; set; }

    [DataTable(BookingRuleFilterFields.BookingUrl, BookingRuleFilterFields.PropertyPaths.BookingUrl)]
    public DataTableFilterItem? BookingUrl { get; set; }

    [DataTable(BookingRuleFilterFields.CreationTime, BookingRuleFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
