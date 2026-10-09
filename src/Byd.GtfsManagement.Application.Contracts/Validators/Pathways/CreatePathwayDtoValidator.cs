using FluentValidation;
using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Pathways;

// mirastan gelen: ortak pathways.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni yuruyus yolu isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni yuruyus yolu olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreatePathwayDtoValidator : PathwayInputDtoValidatorBase<CreatePathwayDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreatePathwayDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(PathwayExceptionCodes.FeedRequired);
    }
}
