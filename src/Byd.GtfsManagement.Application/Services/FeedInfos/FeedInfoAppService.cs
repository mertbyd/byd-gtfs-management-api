using Byd.GtfsManagement.Dtos.FeedInfos;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Interfaces.FeedInfos;
using Byd.GtfsManagement.Managers.FeedInfos;
using Byd.GtfsManagement.Mappers.FeedInfos;
using Byd.GtfsManagement.Models.FeedInfos;

namespace Byd.GtfsManagement.Services.FeedInfos;

// islevi: FeedInfo CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS yayimci bilgisi CRUD servisini ortak tabana baglar.</summary>
public class FeedInfoAppService
    : GtfsCrudAppServiceBase<
        FeedInfo, CreateFeedInfoDto, UpdateFeedInfoDto, FeedInfoDto, FeedInfoFilter,
        FeedInfoCreateModel, FeedInfoUpdateModel, FeedInfoManager, IFeedInfoRepository, FeedInfoMapper>,
        IFeedInfoAppService
{
}
