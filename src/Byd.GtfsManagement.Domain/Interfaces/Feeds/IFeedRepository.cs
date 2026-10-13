using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Feeds;

namespace Byd.GtfsManagement.Interfaces.Feeds;

// islevi: Feed icin core repository sozlesmesini bildirir.
// sistemdeki gorevi: GTFS kayitlarinin kapsam kontrolu Feed'i bu sozlesmeden okur.
public interface IFeedRepository : IExtendedRepository<Feed>
{
}
