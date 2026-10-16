using Byd.GtfsManagement.Dtos.FareRules;

namespace Byd.GtfsManagement.Validators.FareRules;

// mirastan gelen: ortak fare_rules.txt alan kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
// islevi: Yeni ucret kurali istegini dogrular.
// sistemdeki gorevi: Olusturma istegini ortak girdi dogrulamasindan gecirir.
/// <summary>Yeni ucret kurali olusturma dogrulayicisini tanimlar.</summary>
public sealed class CreateFareRuleDtoValidator : FareRuleInputDtoValidatorBase<CreateFareRuleDto>
{
}
