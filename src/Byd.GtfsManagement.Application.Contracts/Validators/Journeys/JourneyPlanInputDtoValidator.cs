using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Journeys;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Journeys;

// islevi: Yolculuk planlama girdisinin durak, tarih, saat bicimi ve aktarma siniri kurallarini dogrular.
// sistemdeki gorevi: Planlama girdisi Manager'a gecmeden sekil kurallarindan gecer.
public sealed class JourneyPlanInputDtoValidator : AbstractValidator<JourneyPlanInputDto>
{
    // Baslangic ve varis duragini, ikisinin farkli olmasini, tarihi, GTFS saat bicimini ve aktarma sinirini kaydeder.
    public JourneyPlanInputDtoValidator()
    {
        RuleFor(input => input.FromStopId)
            .NotEmpty().WithErrorCode(JourneyExceptionCodes.FromStopRequired);
        RuleFor(input => input.ToStopId)
            .NotEmpty().WithErrorCode(JourneyExceptionCodes.ToStopRequired)
            .NotEqual(input => input.FromStopId).WithErrorCode(JourneyExceptionCodes.SameStopForbidden);
        RuleFor(input => input.Date)
            .NotEmpty().WithErrorCode(JourneyExceptionCodes.DateRequired);
        RuleFor(input => input.Time)
            .NotEmpty().WithErrorCode(JourneyExceptionCodes.TimeInvalid)
            .Matches(GtfsTimeConsts.FormatPattern).WithErrorCode(JourneyExceptionCodes.TimeInvalid);
        RuleFor(input => input.MaxTransfers)
            .InclusiveBetween(0, JourneyConsts.MaxTransfersLimit).WithErrorCode(JourneyExceptionCodes.MaxTransfersInvalid);
    }
}
