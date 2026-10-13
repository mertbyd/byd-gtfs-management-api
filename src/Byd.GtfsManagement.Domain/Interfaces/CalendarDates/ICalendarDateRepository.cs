using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.CalendarDates;

namespace Byd.GtfsManagement.Interfaces.CalendarDates;

// islevi: CalendarDate icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface ICalendarDateRepository : IExtendedRepository<CalendarDate>
{
}
