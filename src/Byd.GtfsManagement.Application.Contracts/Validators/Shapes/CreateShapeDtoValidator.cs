using Byd.GtfsManagement.Dtos.Shapes;

namespace Byd.GtfsManagement.Validators.Shapes;

// mirastan gelen: ortak shapes.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni sekil noktasi istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni sekil noktasi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateShapeDtoValidator : ShapeInputDtoValidatorBase<CreateShapeDto>
{
}
