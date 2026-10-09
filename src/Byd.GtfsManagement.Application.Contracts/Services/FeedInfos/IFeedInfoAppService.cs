using Byd.GtfsManagement.Dtos.FeedInfos;

namespace Byd.GtfsManagement.Services.FeedInfos;

// islevi: FeedInfo CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS yayimci bilgisi CRUD API sozlesmesini bildirir.</summary>
public interface IFeedInfoAppService
    : IGtfsCrudAppService<CreateFeedInfoDto, UpdateFeedInfoDto, FeedInfoDto, FeedInfoFilter>
{
}
