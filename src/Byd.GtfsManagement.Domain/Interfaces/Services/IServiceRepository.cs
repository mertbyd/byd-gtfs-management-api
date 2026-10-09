using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Services;

namespace Byd.GtfsManagement.Interfaces.Services;

// islevi: Service icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IServiceRepository : IExtendedRepository<Service>
{
}
