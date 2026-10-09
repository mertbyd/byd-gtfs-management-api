using FluentValidation;
using Byd.GtfsManagement.Dtos.Areas;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Areas;

// mirastan gelen: ortak areas.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni bolge isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni bolge olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateAreaDtoValidator : AreaInputDtoValidatorBase<CreateAreaDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateAreaDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(AreaExceptionCodes.FeedRequired);
    }
}
