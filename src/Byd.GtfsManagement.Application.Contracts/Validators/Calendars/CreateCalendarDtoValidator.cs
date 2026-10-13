using FluentValidation;
using Byd.GtfsManagement.Dtos.Calendars;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Calendars;

// mirastan gelen: gecerlilik araligi zorunluluk kurallari.
// burada yazilan: yalnizca deseni tanimlayan servisin zorunlulugu.
public sealed class CreateCalendarDtoValidator : CalendarInputDtoValidatorBase<CreateCalendarDto>
{
    // Hedef servisin istekte bulunmasini kaydeder.
    public CreateCalendarDtoValidator()
    {
        RuleFor(input => input.ServiceId)
            .NotEmpty().WithErrorCode(CalendarExceptionCodes.ServiceRequired);
    }
}
