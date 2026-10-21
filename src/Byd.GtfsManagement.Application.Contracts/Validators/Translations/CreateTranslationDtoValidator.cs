using Byd.GtfsManagement.Dtos.Translations;

namespace Byd.GtfsManagement.Validators.Translations;

// mirastan gelen: ortak translations.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni ceviri istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni ceviri olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateTranslationDtoValidator : TranslationInputDtoValidatorBase<CreateTranslationDto>
{
}
