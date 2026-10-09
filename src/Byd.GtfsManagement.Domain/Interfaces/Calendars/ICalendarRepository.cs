using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Calendars;

namespace Byd.GtfsManagement.Interfaces.Calendars;

// islevi: Calendar icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface ICalendarRepository : IExtendedRepository<Calendar>
{
}
