using Byd.GtfsManagement.Dtos.Attributions;

namespace Byd.GtfsManagement.Validators.Attributions;

// mirastan gelen: ortak attributions.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni atif istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni atif olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateAttributionDtoValidator : AttributionInputDtoValidatorBase<CreateAttributionDto>
{
}
