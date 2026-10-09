using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.StopAreas;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.StopAreas;

// islevi: StopArea ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS durak bolge iliskisi ortak girdi kurallarini tanimlar.</summary>
public abstract class StopAreaInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : StopAreaInputDto
{
    protected StopAreaInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsAreaId)
            .NotEmpty().WithErrorCode(StopAreaExceptionCodes.GtfsAreaIdRequired)
            .MaximumLength(StopAreaConsts.MaxGtfsAreaIdLength).WithErrorCode(StopAreaExceptionCodes.GtfsAreaIdTooLong);
        RuleFor(input => input.GtfsStopId)
            .NotEmpty().WithErrorCode(StopAreaExceptionCodes.GtfsStopIdRequired)
            .MaximumLength(StopAreaConsts.MaxGtfsStopIdLength).WithErrorCode(StopAreaExceptionCodes.GtfsStopIdTooLong);
    }
}
