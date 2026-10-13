using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Timeframes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Timeframes;

// islevi: Timeframe ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS zaman araligi ortak girdi kurallarini tanimlar.</summary>
public abstract class TimeframeInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : TimeframeInputDto
{
    protected TimeframeInputDtoValidatorBase()
    {
        RuleFor(input => input.TimeframeGroupId)
            .NotEmpty().WithErrorCode(TimeframeExceptionCodes.TimeframeGroupIdRequired)
            .MaximumLength(TimeframeConsts.MaxTimeframeGroupIdLength).WithErrorCode(TimeframeExceptionCodes.TimeframeGroupIdTooLong);
        RuleFor(input => input.StartTime)
            .MaximumLength(TimeframeConsts.MaxStartTimeLength)
            .When(input => !string.IsNullOrEmpty(input.StartTime))
            .WithErrorCode(TimeframeExceptionCodes.StartTimeTooLong);
        RuleFor(input => input.EndTime)
            .MaximumLength(TimeframeConsts.MaxEndTimeLength)
            .When(input => !string.IsNullOrEmpty(input.EndTime))
            .WithErrorCode(TimeframeExceptionCodes.EndTimeTooLong);
        RuleFor(input => input.GtfsServiceId)
            .NotEmpty().WithErrorCode(TimeframeExceptionCodes.GtfsServiceIdRequired)
            .MaximumLength(TimeframeConsts.MaxGtfsServiceIdLength).WithErrorCode(TimeframeExceptionCodes.GtfsServiceIdTooLong);
    }
}
