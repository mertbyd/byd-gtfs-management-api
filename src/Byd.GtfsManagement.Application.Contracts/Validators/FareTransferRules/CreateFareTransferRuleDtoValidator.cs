using FluentValidation;
using Byd.GtfsManagement.Dtos.FareTransferRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareTransferRules;

// mirastan gelen: fare_transfer_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateFareTransferRuleDtoValidator : FareTransferRuleInputDtoValidatorBase<CreateFareTransferRuleDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateFareTransferRuleDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareTransferRuleExceptionCodes.FeedRequired);
    }
}
