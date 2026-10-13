using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Calendars;

// islevi: Calendar liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class CalendarFilter
{
    [DataTable(CalendarFilterFields.FeedId, CalendarFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(CalendarFilterFields.ServiceId, CalendarFilterFields.PropertyPaths.ServiceId)]
    public DataTableFilterItem? ServiceId { get; set; }

    [DataTable(CalendarFilterFields.StartDate, CalendarFilterFields.PropertyPaths.StartDate)]
    public DataTableFilterItem? StartDate { get; set; }

    [DataTable(CalendarFilterFields.EndDate, CalendarFilterFields.PropertyPaths.EndDate)]
    public DataTableFilterItem? EndDate { get; set; }

    [DataTable(CalendarFilterFields.CreationTime, CalendarFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
