using Byd.GtfsManagement.Dtos.BookingRules;

namespace Byd.GtfsManagement.Validators.BookingRules;

// mirastan gelen: booking_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateBookingRuleDtoValidator : BookingRuleInputDtoValidatorBase<CreateBookingRuleDto>
{
}
