using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Transfers;

namespace Byd.GtfsManagement.Interfaces.Transfers;

// islevi: Transfer icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: Aktarma is kurallarini EF uygulamasindan ayirir.
/// <summary>GTFS aktarma veri erisim sozlesmesini bildirir.</summary>
public interface ITransferRepository : IExtendedRepository<Transfer>
{
}
