using Byd.GtfsManagement.Dtos.FeedInfos;

namespace Byd.GtfsManagement.Validators.FeedInfos;

// mirastan gelen: ortak feed_info.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni yayimci bilgisi istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni yayimci bilgisi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFeedInfoDtoValidator : FeedInfoInputDtoValidatorBase<CreateFeedInfoDto>
{
}
