using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareMedias;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareMedias;

// islevi: FareMedia ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret medyasi ortak girdi kurallarini tanimlar.</summary>
public abstract class FareMediaInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareMediaInputDto
{
    protected FareMediaInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsFareMediaId)
            .NotEmpty().WithErrorCode(FareMediaExceptionCodes.GtfsFareMediaIdRequired)
            .MaximumLength(FareMediaConsts.MaxGtfsFareMediaIdLength).WithErrorCode(FareMediaExceptionCodes.GtfsFareMediaIdTooLong);
        RuleFor(input => input.FareMediaName)
            .MaximumLength(FareMediaConsts.MaxFareMediaNameLength)
            .When(input => !string.IsNullOrEmpty(input.FareMediaName))
            .WithErrorCode(FareMediaExceptionCodes.FareMediaNameTooLong);
        RuleFor(input => input.FareMediaType)
            .NotNull().WithErrorCode(FareMediaExceptionCodes.FareMediaTypeRequired);
    }
}
