using Byd.GtfsManagement.Dtos.FareLegJoinRules;

namespace Byd.GtfsManagement.Validators.FareLegJoinRules;

// mirastan gelen: fare_leg_join_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateFareLegJoinRuleDtoValidator : FareLegJoinRuleInputDtoValidatorBase<CreateFareLegJoinRuleDto>
{
}
