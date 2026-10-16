using Byd.GtfsManagement.Dtos.Levels;

namespace Byd.GtfsManagement.Validators.Levels;

// mirastan gelen: ortak levels.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni kat istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni kat olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateLevelDtoValidator : LevelInputDtoValidatorBase<CreateLevelDto>
{
}
