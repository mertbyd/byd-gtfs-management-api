using FluentValidation;
using Byd.GtfsManagement.Dtos.FareLegRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareLegRules;

// mirastan gelen: fare_leg_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateFareLegRuleDtoValidator : FareLegRuleInputDtoValidatorBase<CreateFareLegRuleDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateFareLegRuleDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareLegRuleExceptionCodes.FeedRequired);
    }
}
