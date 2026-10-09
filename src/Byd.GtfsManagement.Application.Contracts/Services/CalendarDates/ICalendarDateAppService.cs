using Byd.GtfsManagement.Dtos.CalendarDates;

namespace Byd.GtfsManagement.Services.CalendarDates;

// islevi: CalendarDate kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface ICalendarDateAppService
    : IGtfsCrudAppService<CreateCalendarDateDto, UpdateCalendarDateDto, CalendarDateDto, CalendarDateFilter>
{
}
