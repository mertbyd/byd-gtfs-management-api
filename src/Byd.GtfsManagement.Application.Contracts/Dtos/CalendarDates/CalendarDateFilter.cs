using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.CalendarDates;

// islevi: CalendarDate liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class CalendarDateFilter : IFeedFilter
{
    [DataTable(CalendarDateFilterFields.FeedId, CalendarDateFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(CalendarDateFilterFields.ServiceId, CalendarDateFilterFields.PropertyPaths.ServiceId)]
    public DataTableFilterItem? ServiceId { get; set; }

    [DataTable(CalendarDateFilterFields.ExceptionTypeId, CalendarDateFilterFields.PropertyPaths.ExceptionTypeId)]
    public DataTableFilterItem? ExceptionTypeId { get; set; }

    [DataTable(CalendarDateFilterFields.Date, CalendarDateFilterFields.PropertyPaths.Date)]
    public DataTableFilterItem? Date { get; set; }

    [DataTable(CalendarDateFilterFields.CreationTime, CalendarDateFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
