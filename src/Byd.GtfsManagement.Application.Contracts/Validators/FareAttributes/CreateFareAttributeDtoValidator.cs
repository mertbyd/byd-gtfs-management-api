using Byd.GtfsManagement.Dtos.FareAttributes;

namespace Byd.GtfsManagement.Validators.FareAttributes;

// mirastan gelen: ortak fare_attributes.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni ucret istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni ucret ozelligi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFareAttributeDtoValidator : FareAttributeInputDtoValidatorBase<CreateFareAttributeDto>
{
}
