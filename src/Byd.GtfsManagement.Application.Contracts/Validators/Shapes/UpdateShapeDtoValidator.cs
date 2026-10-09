using Byd.GtfsManagement.Dtos.Shapes;

namespace Byd.GtfsManagement.Validators.Shapes;

// mirastan gelen: ortak shapes.txt alan kurallari.
// islevi: UpdateShapeDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS sekil guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateShapeDtoValidator : ShapeInputDtoValidatorBase<UpdateShapeDto>
{
}
