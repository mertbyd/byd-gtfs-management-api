using Byd.GtfsManagement.Dtos.Pathways;

namespace Byd.GtfsManagement.Validators.Pathways;

// mirastan gelen: ortak pathways.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni yuruyus yolu istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni yuruyus yolu olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreatePathwayDtoValidator : PathwayInputDtoValidatorBase<CreatePathwayDto>
{
}
