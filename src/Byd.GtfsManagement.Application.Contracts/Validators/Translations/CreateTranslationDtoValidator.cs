using FluentValidation;
using Byd.GtfsManagement.Dtos.Translations;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Translations;

// mirastan gelen: ortak translations.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni ceviri isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni ceviri olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateTranslationDtoValidator : TranslationInputDtoValidatorBase<CreateTranslationDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateTranslationDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(TranslationExceptionCodes.FeedRequired);
    }
}
