using FluentValidation;
using Byd.GtfsManagement.Dtos.BookingRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.BookingRules;

// mirastan gelen: booking_rules.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateBookingRuleDtoValidator : BookingRuleInputDtoValidatorBase<CreateBookingRuleDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateBookingRuleDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(BookingRuleExceptionCodes.FeedRequired);
    }
}
