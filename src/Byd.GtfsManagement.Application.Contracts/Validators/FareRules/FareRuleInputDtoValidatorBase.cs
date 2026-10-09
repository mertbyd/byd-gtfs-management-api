using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareRules;

// islevi: fare_rules.txt ortak ucret tanimi, guzergah ve bolge kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret kurali ortak girdi kurallarini tanimlar.</summary>
public abstract class FareRuleInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareRuleInputDto
{
    // Ucret tanimi, guzergah ve bolge kurallarini kaydeder.
    protected FareRuleInputDtoValidatorBase()
    {
        RuleFor(input => input.FareAttributeId)
            .NotEmpty().WithErrorCode(FareRuleExceptionCodes.FareRequired);

        RuleFor(input => input.OriginId)
            .MaximumLength(FareRuleConsts.MaxZoneIdLength)
            .When(input => !string.IsNullOrEmpty(input.OriginId))
            .WithErrorCode(FareRuleExceptionCodes.OriginIdTooLong);

        RuleFor(input => input.DestinationId)
            .MaximumLength(FareRuleConsts.MaxZoneIdLength)
            .When(input => !string.IsNullOrEmpty(input.DestinationId))
            .WithErrorCode(FareRuleExceptionCodes.DestinationIdTooLong);

        RuleFor(input => input.ContainsId)
            .MaximumLength(FareRuleConsts.MaxZoneIdLength)
            .When(input => !string.IsNullOrEmpty(input.ContainsId))
            .WithErrorCode(FareRuleExceptionCodes.ContainsIdTooLong);
    }
}
