using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.StopTimes;

namespace Byd.GtfsManagement.Interfaces.StopTimes;

// islevi: StopTime icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Domain kurallarini EF uygulamasindan ayirir.
/// <summary>StopTime veri erisim sozlesmesini bildirir.</summary>
public interface IStopTimeRepository : IExtendedRepository<StopTime>
{
}
