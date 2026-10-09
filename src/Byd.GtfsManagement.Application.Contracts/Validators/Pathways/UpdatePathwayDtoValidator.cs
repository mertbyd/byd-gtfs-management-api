using Byd.GtfsManagement.Dtos.Pathways;

namespace Byd.GtfsManagement.Validators.Pathways;

// mirastan gelen: ortak pathways.txt alan kurallari.
// islevi: UpdatePathwayDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS yuruyus yolu guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdatePathwayDtoValidator : PathwayInputDtoValidatorBase<UpdatePathwayDto>
{
}
