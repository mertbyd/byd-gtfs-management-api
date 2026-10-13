using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.FeedInfos;

namespace Byd.GtfsManagement.Interfaces.FeedInfos;

// islevi: FeedInfo tablosuna ozgu sorgu sozlesmesini bildirir.
// sistemdeki gorevi: Domain katmanini EF persistence uygulamasindan ayirir.
/// <summary>GTFS yayimci bilgisi repository sozlesmesini tanimlar.</summary>
public interface IFeedInfoRepository : IExtendedRepository<FeedInfo>
{
}
