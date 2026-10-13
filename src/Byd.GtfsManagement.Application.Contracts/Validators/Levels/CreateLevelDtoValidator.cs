using FluentValidation;
using Byd.GtfsManagement.Dtos.Levels;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Levels;

// mirastan gelen: ortak levels.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni kat isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni kat olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateLevelDtoValidator : LevelInputDtoValidatorBase<CreateLevelDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateLevelDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(LevelExceptionCodes.FeedRequired);
    }
}
