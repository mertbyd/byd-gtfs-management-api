using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Routes;

// islevi: routes.txt alanlarinin zorunluluk, uzunluk, renk bicimi ve sira kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class RouteInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : RouteInputDto
{
    // GTFS guzergah alanlarinin sekil kurallarini kaydeder.
    protected RouteInputDtoValidatorBase()
    {
        RuleFor(input => input.RouteTypeId)
            .GreaterThan(0).WithErrorCode(RouteExceptionCodes.RouteTypeRequired);
        RuleFor(input => input.GtfsRouteId)
            .NotEmpty().WithErrorCode(RouteExceptionCodes.GtfsRouteIdRequired)
            .MaximumLength(RouteConsts.MaxGtfsRouteIdLength).WithErrorCode(RouteExceptionCodes.GtfsRouteIdTooLong);
        RuleFor(input => input.ShortName)
            .MaximumLength(RouteConsts.MaxShortNameLength).WithErrorCode(RouteExceptionCodes.ShortNameTooLong);
        RuleFor(input => input.LongName)
            .MaximumLength(RouteConsts.MaxLongNameLength).WithErrorCode(RouteExceptionCodes.LongNameTooLong);
        RuleFor(input => input.Description)
            .MaximumLength(RouteConsts.MaxDescriptionLength).WithErrorCode(RouteExceptionCodes.DescriptionTooLong);
        RuleFor(input => input.Url)
            .MaximumLength(RouteConsts.MaxUrlLength).WithErrorCode(RouteExceptionCodes.UrlTooLong);
        RuleFor(input => input.Color)
            .Matches(RouteConsts.ColorPattern).WithErrorCode(RouteExceptionCodes.ColorInvalid)
            .When(input => !string.IsNullOrWhiteSpace(input.Color));
        RuleFor(input => input.TextColor)
            .Matches(RouteConsts.ColorPattern).WithErrorCode(RouteExceptionCodes.TextColorInvalid)
            .When(input => !string.IsNullOrWhiteSpace(input.TextColor));
        RuleFor(input => input.SortOrder)
            .GreaterThanOrEqualTo(0).WithErrorCode(RouteExceptionCodes.SortOrderNegative)
            .When(input => input.SortOrder is not null);
    }
}
