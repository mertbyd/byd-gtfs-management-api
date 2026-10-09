using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.LocationGroups;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.LocationGroups;

// islevi: LocationGroup ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS konum grubu ortak girdi kurallarini tanimlar.</summary>
public abstract class LocationGroupInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : LocationGroupInputDto
{
    protected LocationGroupInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsLocationGroupId)
            .NotEmpty().WithErrorCode(LocationGroupExceptionCodes.GtfsLocationGroupIdRequired)
            .MaximumLength(LocationGroupConsts.MaxGtfsLocationGroupIdLength).WithErrorCode(LocationGroupExceptionCodes.GtfsLocationGroupIdTooLong);
        RuleFor(input => input.LocationGroupName)
            .MaximumLength(LocationGroupConsts.MaxLocationGroupNameLength)
            .When(input => !string.IsNullOrEmpty(input.LocationGroupName))
            .WithErrorCode(LocationGroupExceptionCodes.LocationGroupNameTooLong);
    }
}
