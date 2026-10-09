using FluentValidation;
using Byd.GtfsManagement.Dtos.FareAttributes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareAttributes;

// mirastan gelen: ortak fare_attributes.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni ucret isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni ucret ozelligi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFareAttributeDtoValidator : FareAttributeInputDtoValidatorBase<CreateFareAttributeDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateFareAttributeDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareAttributeExceptionCodes.FeedRequired);
    }
}
