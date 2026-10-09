using Byd.GtfsManagement.Dtos.Attributions;

namespace Byd.GtfsManagement.Validators.Attributions;

// mirastan gelen: ortak attributions.txt alan kurallari.
// islevi: UpdateAttributionDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak dogrulama kurallariyla kontrol eder.
/// <summary>GTFS atif guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateAttributionDtoValidator : AttributionInputDtoValidatorBase<UpdateAttributionDto>
{
}
