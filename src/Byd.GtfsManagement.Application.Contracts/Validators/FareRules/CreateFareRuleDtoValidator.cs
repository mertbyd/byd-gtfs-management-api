using FluentValidation;
using Byd.GtfsManagement.Dtos.FareRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareRules;

// mirastan gelen: ortak fare_rules.txt alan kurallari.
// burada yazilan: Feed zorunlulugu.
// islevi: Yeni ucret kurali isteginin kapsam anahtarini dogrular.
// sistemdeki gorevi: Manager'a Feedsuz kayit ulasmasini engeller.
/// <summary>Yeni ucret kurali olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFareRuleDtoValidator : FareRuleInputDtoValidatorBase<CreateFareRuleDto>
{
    // Yeni kaydin Feed bagini zorunlu tutar.
    public CreateFareRuleDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(FareRuleExceptionCodes.FeedRequired);
    }
}
