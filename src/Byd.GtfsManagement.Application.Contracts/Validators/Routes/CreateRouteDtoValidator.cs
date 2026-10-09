using FluentValidation;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Routes;

// mirastan gelen: routes.txt alanlarinin zorunluluk, uzunluk, renk ve sira kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateRouteDtoValidator : RouteInputDtoValidatorBase<CreateRouteDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateRouteDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(RouteExceptionCodes.FeedRequired);
    }
}
