using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Stops;

// islevi: Durak kalkis sorgusunun servis gunu, saat bicimi ve kalkis siniri kurallarini dogrular.
// sistemdeki gorevi: Kalkis ucunun girdisi Manager'a gecmeden sekil kurallarindan gecer.
public sealed class StopDepartureInputDtoValidator : AbstractValidator<StopDepartureInputDto>
{
    // Servis gunu zorunlulugunu, GTFS saat bicimini ve kalkis sinirini kaydeder.
    public StopDepartureInputDtoValidator()
    {
        RuleFor(input => input.Date)
            .NotEmpty().WithErrorCode(StopExceptionCodes.DepartureDateRequired);
        RuleFor(input => input.FromTime)
            .Matches(GtfsTimeConsts.FormatPattern).WithErrorCode(StopExceptionCodes.DepartureTimeInvalid)
            .When(input => input.FromTime is not null);
        RuleFor(input => input.Limit)
            .InclusiveBetween(1, StopConsts.MaxDepartureLimit).WithErrorCode(StopExceptionCodes.DepartureLimitInvalid);
    }
}
