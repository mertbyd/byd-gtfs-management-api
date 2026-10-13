using FluentValidation;
using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Shapes;

// mirastan gelen: ortak shapes.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni sekil noktasi isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni sekil noktasi olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateShapeDtoValidator : ShapeInputDtoValidatorBase<CreateShapeDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateShapeDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(ShapeExceptionCodes.FeedRequired);
    }
}
