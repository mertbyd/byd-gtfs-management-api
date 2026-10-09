using Byd.GtfsManagement.Dtos.Areas;

namespace Byd.GtfsManagement.Validators.Areas;

// mirastan gelen: ortak areas.txt alan kurallari.
// islevi: UpdateAreaDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak dogrulama kurallariyla kontrol eder.
/// <summary>GTFS bolge guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateAreaDtoValidator : AreaInputDtoValidatorBase<UpdateAreaDto>
{
}
