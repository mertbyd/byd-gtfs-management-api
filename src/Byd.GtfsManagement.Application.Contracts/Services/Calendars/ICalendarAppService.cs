using Byd.GtfsManagement.Dtos.Calendars;

namespace Byd.GtfsManagement.Services.Calendars;

// islevi: Calendar kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface ICalendarAppService
    : IGtfsCrudAppService<CreateCalendarDto, UpdateCalendarDto, CalendarDto, CalendarFilter>
{
}
