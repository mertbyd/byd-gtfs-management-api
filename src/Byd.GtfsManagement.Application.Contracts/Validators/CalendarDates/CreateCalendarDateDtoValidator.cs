using FluentValidation;
using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.CalendarDates;

// mirastan gelen: istisna turu ve gun zorunluluk kurallari.
// burada yazilan: yalnizca istisnanin uygulandigi servisin zorunlulugu.
public sealed class CreateCalendarDateDtoValidator : CalendarDateInputDtoValidatorBase<CreateCalendarDateDto>
{
    // Hedef servisin istekte bulunmasini kaydeder.
    public CreateCalendarDateDtoValidator()
    {
        RuleFor(input => input.ServiceId)
            .NotEmpty().WithErrorCode(CalendarDateExceptionCodes.ServiceRequired);
    }
}
