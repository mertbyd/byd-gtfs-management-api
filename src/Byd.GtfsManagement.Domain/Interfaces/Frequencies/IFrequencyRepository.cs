using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Frequencies;

namespace Byd.GtfsManagement.Interfaces.Frequencies;

// islevi: Frequency icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
/// <summary>Sefer sikligi veri erisim sozlesmesini bildirir.</summary>
public interface IFrequencyRepository : IExtendedRepository<Frequency>
{
}
