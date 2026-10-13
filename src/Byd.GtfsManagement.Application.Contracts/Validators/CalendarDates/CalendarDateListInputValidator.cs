using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.CalendarDates;

namespace Byd.GtfsManagement.Validators.CalendarDates;

// islevi: CalendarDate liste isteginin sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu repository'sine yalnizca sinirlari belli liste isteklerinin girmesini saglar.
public sealed class CalendarDateListInputValidator : AbstractValidator<DataTableListInput<CalendarDateFilter>>
{
    // Sayfa numarasi ve sayfa boyutu kurallarini kaydeder.
    public CalendarDateListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
