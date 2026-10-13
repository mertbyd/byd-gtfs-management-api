using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Calendars;

namespace Byd.GtfsManagement.Validators.Calendars;

// islevi: Calendar liste isteginin sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu repository'sine yalnizca sinirlari belli liste isteklerinin girmesini saglar.
public sealed class CalendarListInputValidator : AbstractValidator<DataTableListInput<CalendarFilter>>
{
    // Sayfa numarasi ve sayfa boyutu kurallarini kaydeder.
    public CalendarListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
