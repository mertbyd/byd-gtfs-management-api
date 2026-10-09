using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Stops;

namespace Byd.GtfsManagement.Interfaces.Stops;

// islevi: Stop icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IStopRepository : IExtendedRepository<Stop>
{
}
