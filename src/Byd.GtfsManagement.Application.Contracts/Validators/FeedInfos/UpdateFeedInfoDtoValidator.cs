using Byd.GtfsManagement.Dtos.FeedInfos;

namespace Byd.GtfsManagement.Validators.FeedInfos;

// mirastan gelen: ortak feed_info.txt alan kurallari.
// islevi: UpdateFeedInfoDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS yayimci bilgisi guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateFeedInfoDtoValidator : FeedInfoInputDtoValidatorBase<UpdateFeedInfoDto>
{
}
