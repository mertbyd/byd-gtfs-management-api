using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Agencies;

namespace Byd.GtfsManagement.Interfaces.Agencies;

// islevi: Agency icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
public interface IAgencyRepository : IExtendedRepository<Agency>
{
}
