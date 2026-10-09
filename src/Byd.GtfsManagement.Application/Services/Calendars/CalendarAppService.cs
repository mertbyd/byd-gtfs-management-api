using Byd.GtfsManagement.Dtos.Calendars;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Interfaces.Calendars;
using Byd.GtfsManagement.Managers.Calendars;
using Byd.GtfsManagement.Mappers.Calendars;
using Byd.GtfsManagement.Models.Calendars;

namespace Byd.GtfsManagement.Services.Calendars;

// islevi: Calendar kayitlarinin CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class CalendarAppService
    : GtfsCrudAppServiceBase<
        Calendar, CreateCalendarDto, UpdateCalendarDto, CalendarDto, CalendarFilter,
        CalendarCreateModel, CalendarUpdateModel, CalendarManager, ICalendarRepository, CalendarMapper>, ICalendarAppService
{
}
