using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareTransferRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareTransferRules;

// islevi: FareTransferRule ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret aktarma kurali ortak girdi kurallarini tanimlar.</summary>
public abstract class FareTransferRuleInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareTransferRuleInputDto
{
    protected FareTransferRuleInputDtoValidatorBase()
    {
        RuleFor(input => input.FromLegGroupId)
            .MaximumLength(FareTransferRuleConsts.MaxFromLegGroupIdLength)
            .When(input => !string.IsNullOrEmpty(input.FromLegGroupId))
            .WithErrorCode(FareTransferRuleExceptionCodes.FromLegGroupIdTooLong);
        RuleFor(input => input.ToLegGroupId)
            .MaximumLength(FareTransferRuleConsts.MaxToLegGroupIdLength)
            .When(input => !string.IsNullOrEmpty(input.ToLegGroupId))
            .WithErrorCode(FareTransferRuleExceptionCodes.ToLegGroupIdTooLong);
        RuleFor(input => input.FareTransferType)
            .NotNull().WithErrorCode(FareTransferRuleExceptionCodes.FareTransferTypeRequired);
        RuleFor(input => input.GtfsFareProductId)
            .MaximumLength(FareTransferRuleConsts.MaxGtfsFareProductIdLength)
            .When(input => !string.IsNullOrEmpty(input.GtfsFareProductId))
            .WithErrorCode(FareTransferRuleExceptionCodes.GtfsFareProductIdTooLong);
    }
}
