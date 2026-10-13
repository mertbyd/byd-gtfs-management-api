using FluentValidation;
using Byd.GtfsManagement.Dtos.Attributions;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Attributions;

// mirastan gelen: ortak attributions.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni atif isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni atif olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateAttributionDtoValidator : AttributionInputDtoValidatorBase<CreateAttributionDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateAttributionDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(AttributionExceptionCodes.FeedRequired);
    }
}
