using Byd.GtfsManagement.Dtos.Levels;

namespace Byd.GtfsManagement.Validators.Levels;

// mirastan gelen: ortak levels.txt alan kurallari.
// islevi: UpdateLevelDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS kat guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateLevelDtoValidator : LevelInputDtoValidatorBase<UpdateLevelDto>
{
}
