using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.RouteNetworks;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.RouteNetworks;

// islevi: RouteNetwork ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS hat ag iliskisi ortak girdi kurallarini tanimlar.</summary>
public abstract class RouteNetworkInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : RouteNetworkInputDto
{
    protected RouteNetworkInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsNetworkId)
            .NotEmpty().WithErrorCode(RouteNetworkExceptionCodes.GtfsNetworkIdRequired)
            .MaximumLength(RouteNetworkConsts.MaxGtfsNetworkIdLength).WithErrorCode(RouteNetworkExceptionCodes.GtfsNetworkIdTooLong);
        RuleFor(input => input.GtfsRouteId)
            .NotEmpty().WithErrorCode(RouteNetworkExceptionCodes.GtfsRouteIdRequired)
            .MaximumLength(RouteNetworkConsts.MaxGtfsRouteIdLength).WithErrorCode(RouteNetworkExceptionCodes.GtfsRouteIdTooLong);
    }
}
