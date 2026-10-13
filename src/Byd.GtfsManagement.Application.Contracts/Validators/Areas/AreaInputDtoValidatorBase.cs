using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Areas;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Areas;

// islevi: areas.txt ortak bolge kimligi ve bolge adi kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS bolge ortak girdi kurallarini tanimlar.</summary>
public abstract class AreaInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : AreaInputDto
{
    // Bolge kimligi ve bolge adi kurallarini kaydeder.
    protected AreaInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsAreaId)
            .NotEmpty().WithErrorCode(AreaExceptionCodes.AreaIdRequired)
            .MaximumLength(AreaConsts.MaxGtfsAreaIdLength).WithErrorCode(AreaExceptionCodes.AreaIdTooLong);

        RuleFor(input => input.AreaName)
            .MaximumLength(AreaConsts.MaxAreaNameLength)
            .When(input => !string.IsNullOrEmpty(input.AreaName))
            .WithErrorCode(AreaExceptionCodes.AreaNameTooLong);
    }
}
