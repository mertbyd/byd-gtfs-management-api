using Byd.GtfsManagement.Dtos.FareTransferRules;

namespace Byd.GtfsManagement.Validators.FareTransferRules;

// mirastan gelen: fare_transfer_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateFareTransferRuleDtoValidator : FareTransferRuleInputDtoValidatorBase<CreateFareTransferRuleDto>
{
}
