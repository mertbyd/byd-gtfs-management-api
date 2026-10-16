using Byd.GtfsManagement.Dtos.Areas;

namespace Byd.GtfsManagement.Validators.Areas;

// mirastan gelen: ortak areas.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni bolge istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni bolge olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateAreaDtoValidator : AreaInputDtoValidatorBase<CreateAreaDto>
{
}
