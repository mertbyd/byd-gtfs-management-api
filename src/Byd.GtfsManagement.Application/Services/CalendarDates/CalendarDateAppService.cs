using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Interfaces.CalendarDates;
using Byd.GtfsManagement.Managers.CalendarDates;
using Byd.GtfsManagement.Mappers.CalendarDates;
using Byd.GtfsManagement.Models.CalendarDates;

namespace Byd.GtfsManagement.Services.CalendarDates;

// islevi: CalendarDate kayitlarinin CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli baglari bildirir; dogrulama, Manager, Mapperly ve kalici yazma tabanda kalir.
public class CalendarDateAppService
    : GtfsImportExportAppServiceBase<
        CalendarDate, CreateCalendarDateDto, UpdateCalendarDateDto, CalendarDateDto, CalendarDateFilter,
        CalendarDateCreateModel, CalendarDateUpdateModel, CalendarDateManager, ICalendarDateRepository, CalendarDateMapper, CalendarDateImportDto>, ICalendarDateAppService
{
}
