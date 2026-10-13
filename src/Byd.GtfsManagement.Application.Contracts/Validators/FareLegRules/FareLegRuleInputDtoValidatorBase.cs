using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareLegRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareLegRules;

// islevi: FareLegRule ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret etap kurali ortak girdi kurallarini tanimlar.</summary>
public abstract class FareLegRuleInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareLegRuleInputDto
{
    protected FareLegRuleInputDtoValidatorBase()
    {
        RuleFor(input => input.LegGroupId)
            .MaximumLength(FareLegRuleConsts.MaxLegGroupIdLength)
            .When(input => !string.IsNullOrEmpty(input.LegGroupId))
            .WithErrorCode(FareLegRuleExceptionCodes.LegGroupIdTooLong);
        RuleFor(input => input.NetworkId)
            .MaximumLength(FareLegRuleConsts.MaxNetworkIdLength)
            .When(input => !string.IsNullOrEmpty(input.NetworkId))
            .WithErrorCode(FareLegRuleExceptionCodes.NetworkIdTooLong);
        RuleFor(input => input.FromAreaId)
            .MaximumLength(FareLegRuleConsts.MaxFromAreaIdLength)
            .When(input => !string.IsNullOrEmpty(input.FromAreaId))
            .WithErrorCode(FareLegRuleExceptionCodes.FromAreaIdTooLong);
        RuleFor(input => input.ToAreaId)
            .MaximumLength(FareLegRuleConsts.MaxToAreaIdLength)
            .When(input => !string.IsNullOrEmpty(input.ToAreaId))
            .WithErrorCode(FareLegRuleExceptionCodes.ToAreaIdTooLong);
        RuleFor(input => input.FromTimeframeGroupId)
            .MaximumLength(FareLegRuleConsts.MaxFromTimeframeGroupIdLength)
            .When(input => !string.IsNullOrEmpty(input.FromTimeframeGroupId))
            .WithErrorCode(FareLegRuleExceptionCodes.FromTimeframeGroupIdTooLong);
        RuleFor(input => input.ToTimeframeGroupId)
            .MaximumLength(FareLegRuleConsts.MaxToTimeframeGroupIdLength)
            .When(input => !string.IsNullOrEmpty(input.ToTimeframeGroupId))
            .WithErrorCode(FareLegRuleExceptionCodes.ToTimeframeGroupIdTooLong);
        RuleFor(input => input.GtfsFareProductId)
            .NotEmpty().WithErrorCode(FareLegRuleExceptionCodes.GtfsFareProductIdRequired)
            .MaximumLength(FareLegRuleConsts.MaxGtfsFareProductIdLength).WithErrorCode(FareLegRuleExceptionCodes.GtfsFareProductIdTooLong);
    }
}
