using Byd.GtfsManagement.Dtos.FareRules;

namespace Byd.GtfsManagement.Validators.FareRules;

// mirastan gelen: ortak fare_rules.txt alan kurallari.
// islevi: UpdateFareRuleDto kurallarini tanimlar.
// sistemdeki gorevi: Guncelleme istegini ortak girdi dogrulamasindan gecirir.
/// <summary>GTFS ucret kurali guncelleme dogrulayicisini tanimlar.</summary>
public sealed class UpdateFareRuleDtoValidator : FareRuleInputDtoValidatorBase<UpdateFareRuleDto>
{
}
