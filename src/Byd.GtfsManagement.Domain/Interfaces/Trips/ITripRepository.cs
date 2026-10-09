using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Trips;

namespace Byd.GtfsManagement.Interfaces.Trips;

// islevi: Trip icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface ITripRepository : IExtendedRepository<Trip>
{
}
