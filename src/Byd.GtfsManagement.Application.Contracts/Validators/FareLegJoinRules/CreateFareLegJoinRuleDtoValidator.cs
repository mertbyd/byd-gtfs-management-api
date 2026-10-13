using FluentValidation;
using Byd.GtfsManagement.Dtos.FareLegJoinRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareLegJoinRules;

// mirastan gelen: fare_leg_join_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateFareLegJoinRuleDtoValidator : FareLegJoinRuleInputDtoValidatorBase<CreateFareLegJoinRuleDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateFareLegJoinRuleDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareLegJoinRuleExceptionCodes.FeedRequired);
    }
}
