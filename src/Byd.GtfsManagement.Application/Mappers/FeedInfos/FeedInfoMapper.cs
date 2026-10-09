using Byd.GtfsManagement.Dtos.FeedInfos;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Models.FeedInfos;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.FeedInfos;

// islevi: FeedInfo DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS yayimci bilgisi DTO ve modellerini esler.</summary>
[Mapper]
public partial class FeedInfoMapper
    : IGtfsCrudMapper<FeedInfo, CreateFeedInfoDto, UpdateFeedInfoDto, FeedInfoDto,
        FeedInfoCreateModel, FeedInfoUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial FeedInfoCreateModel Map(CreateFeedInfoDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial FeedInfoUpdateModel Map(UpdateFeedInfoDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial FeedInfoDto Map(FeedInfo entity);
}
