using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.LocationGroupStops;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.LocationGroupStops;

// islevi: LocationGroupStop ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS konum grubu durak iliskisi ortak girdi kurallarini tanimlar.</summary>
public abstract class LocationGroupStopInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : LocationGroupStopInputDto
{
    protected LocationGroupStopInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsLocationGroupId)
            .NotEmpty().WithErrorCode(LocationGroupStopExceptionCodes.GtfsLocationGroupIdRequired)
            .MaximumLength(LocationGroupStopConsts.MaxGtfsLocationGroupIdLength).WithErrorCode(LocationGroupStopExceptionCodes.GtfsLocationGroupIdTooLong);
        RuleFor(input => input.GtfsStopId)
            .NotEmpty().WithErrorCode(LocationGroupStopExceptionCodes.GtfsStopIdRequired)
            .MaximumLength(LocationGroupStopConsts.MaxGtfsStopIdLength).WithErrorCode(LocationGroupStopExceptionCodes.GtfsStopIdTooLong);
    }
}
