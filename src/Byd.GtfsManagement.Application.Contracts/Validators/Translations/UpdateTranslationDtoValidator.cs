using Byd.GtfsManagement.Dtos.Translations;

namespace Byd.GtfsManagement.Validators.Translations;

// mirastan gelen: ortak translations.txt alan kurallari.
// islevi: UpdateTranslationDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak dogrulama kurallariyla kontrol eder.
/// <summary>GTFS ceviri guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateTranslationDtoValidator : TranslationInputDtoValidatorBase<UpdateTranslationDto>
{
}
