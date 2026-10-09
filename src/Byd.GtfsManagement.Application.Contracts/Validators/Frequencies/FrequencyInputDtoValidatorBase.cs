using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Frequencies;

// islevi: frequencies.txt ortak sekil ve zorunlu alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>Sefer sikligi ortak girdi kurallarini tanimlar.</summary>
public abstract class FrequencyInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FrequencyInputDto
{
    // GTFS saat bicimi, pozitif headway ve exact_times kodlarini kaydeder.
    protected FrequencyInputDtoValidatorBase()
    {
        RuleFor(input => input.StartTime)
            .NotEmpty().WithErrorCode(FrequencyExceptionCodes.StartTimeRequired)
            .Matches(GtfsTimeConsts.FormatPattern).WithErrorCode(FrequencyExceptionCodes.TimeInvalid);
        RuleFor(input => input.EndTime)
            .NotEmpty().WithErrorCode(FrequencyExceptionCodes.EndTimeRequired)
            .Matches(GtfsTimeConsts.FormatPattern).WithErrorCode(FrequencyExceptionCodes.TimeInvalid);
        RuleFor(input => input.HeadwaySecs)
            .GreaterThan(0).WithErrorCode(FrequencyExceptionCodes.HeadwayInvalid);
        RuleFor(input => input.ExactTimes)
            .Must(value => value is null
                or FrequencyConsts.FrequencyBasedCode
                or FrequencyConsts.ScheduleBasedCode)
            .WithErrorCode(FrequencyExceptionCodes.ExactTimesInvalid);
    }
}
