using System;
using FluentValidation;
using Byd.GtfsManagement.Dtos.CalendarDates;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.CalendarDates;

// islevi: calendar_dates.txt istisna turu ve gun alanlarinin zorunluluk kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class CalendarDateInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : CalendarDateInputDto
{
    // Istisna turu ve gun alanlarinin dolu gelmesini kaydeder.
    protected CalendarDateInputDtoValidatorBase()
    {
        RuleFor(input => input.ExceptionTypeId)
            .GreaterThan(0).WithErrorCode(CalendarDateExceptionCodes.ExceptionTypeRequired);
        RuleFor(input => input.Date)
            .NotEqual(default(DateOnly)).WithErrorCode(CalendarDateExceptionCodes.DateRequired);
    }
}
