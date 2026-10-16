using Byd.GtfsManagement.Dtos.FareLegRules;

namespace Byd.GtfsManagement.Validators.FareLegRules;

// mirastan gelen: fare_leg_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateFareLegRuleDtoValidator : FareLegRuleInputDtoValidatorBase<CreateFareLegRuleDto>
{
}
