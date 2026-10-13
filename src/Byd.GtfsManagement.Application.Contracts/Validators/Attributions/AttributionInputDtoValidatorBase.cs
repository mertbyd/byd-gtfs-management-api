using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Attributions;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Attributions;

// islevi: attributions.txt ortak kurum, rol, iletisim ve dis kimlik kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS atif ortak girdi kurallarini tanimlar.</summary>
public abstract class AttributionInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : AttributionInputDto
{
    // Kurum, dis kimlik, rol ve iletisim kurallarini kaydeder.
    protected AttributionInputDtoValidatorBase()
    {
        RuleFor(input => input.OrganizationName)
            .NotEmpty().WithErrorCode(AttributionExceptionCodes.OrganizationNameRequired)
            .MaximumLength(AttributionConsts.MaxOrganizationNameLength).WithErrorCode(AttributionExceptionCodes.OrganizationNameTooLong);

        RuleFor(input => input.GtfsAttributionId)
            .MaximumLength(AttributionConsts.MaxGtfsAttributionIdLength)
            .When(input => !string.IsNullOrEmpty(input.GtfsAttributionId))
            .WithErrorCode(AttributionExceptionCodes.AttributionIdTooLong);

        RuleFor(input => input.AttributionUrl)
            .MaximumLength(AttributionConsts.MaxAttributionUrlLength)
            .When(input => !string.IsNullOrEmpty(input.AttributionUrl))
            .WithErrorCode(AttributionExceptionCodes.AttributionUrlTooLong);

        RuleFor(input => input.AttributionEmail)
            .MaximumLength(AttributionConsts.MaxAttributionEmailLength)
            .When(input => !string.IsNullOrEmpty(input.AttributionEmail))
            .WithErrorCode(AttributionExceptionCodes.AttributionEmailTooLong);

        RuleFor(input => input.AttributionPhone)
            .MaximumLength(AttributionConsts.MaxAttributionPhoneLength)
            .When(input => !string.IsNullOrEmpty(input.AttributionPhone))
            .WithErrorCode(AttributionExceptionCodes.AttributionPhoneTooLong);

        RuleFor(input => input.IsProducer)
            .Must(val => val is null or 0 or 1)
            .WithErrorCode(AttributionExceptionCodes.InvalidFlagValue);

        RuleFor(input => input.IsOperator)
            .Must(val => val is null or 0 or 1)
            .WithErrorCode(AttributionExceptionCodes.InvalidFlagValue);

        RuleFor(input => input.IsAuthority)
            .Must(val => val is null or 0 or 1)
            .WithErrorCode(AttributionExceptionCodes.InvalidFlagValue);
    }
}
