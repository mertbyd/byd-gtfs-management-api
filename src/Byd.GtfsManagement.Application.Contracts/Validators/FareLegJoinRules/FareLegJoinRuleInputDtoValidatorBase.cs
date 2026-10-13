using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareLegJoinRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareLegJoinRules;

// islevi: FareLegJoinRule ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret etap birlestirme kurali ortak girdi kurallarini tanimlar.</summary>
public abstract class FareLegJoinRuleInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareLegJoinRuleInputDto
{
    protected FareLegJoinRuleInputDtoValidatorBase()
    {
        RuleFor(input => input.FromNetworkId)
            .MaximumLength(FareLegJoinRuleConsts.MaxFromNetworkIdLength)
            .When(input => !string.IsNullOrEmpty(input.FromNetworkId))
            .WithErrorCode(FareLegJoinRuleExceptionCodes.FromNetworkIdTooLong);
        RuleFor(input => input.ToNetworkId)
            .MaximumLength(FareLegJoinRuleConsts.MaxToNetworkIdLength)
            .When(input => !string.IsNullOrEmpty(input.ToNetworkId))
            .WithErrorCode(FareLegJoinRuleExceptionCodes.ToNetworkIdTooLong);
        RuleFor(input => input.FromStopId)
            .MaximumLength(FareLegJoinRuleConsts.MaxFromStopIdLength)
            .When(input => !string.IsNullOrEmpty(input.FromStopId))
            .WithErrorCode(FareLegJoinRuleExceptionCodes.FromStopIdTooLong);
        RuleFor(input => input.ToStopId)
            .MaximumLength(FareLegJoinRuleConsts.MaxToStopIdLength)
            .When(input => !string.IsNullOrEmpty(input.ToStopId))
            .WithErrorCode(FareLegJoinRuleExceptionCodes.ToStopIdTooLong);
    }
}
