using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Routes;

namespace Byd.GtfsManagement.Interfaces.Routes;

// islevi: Route icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IRouteRepository : IExtendedRepository<Route>
{
}
