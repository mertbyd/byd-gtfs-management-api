using System;
using FluentValidation;
using Byd.GtfsManagement.Dtos.Calendars;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Calendars;

// islevi: calendar.txt tarih alanlarinin zorunluluk kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class CalendarInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : CalendarInputDto
{
    // Gecerlilik araligi alanlarinin dolu gelmesini kaydeder.
    protected CalendarInputDtoValidatorBase()
    {
        RuleFor(input => input.StartDate)
            .NotEqual(default(DateOnly)).WithErrorCode(CalendarExceptionCodes.StartDateRequired);
        RuleFor(input => input.EndDate)
            .NotEqual(default(DateOnly)).WithErrorCode(CalendarExceptionCodes.EndDateRequired);
    }
}
