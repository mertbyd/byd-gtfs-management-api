using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.StopTimes;

namespace Byd.GtfsManagement.Validators.StopTimes;

// islevi: StopTime filtreli liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirlari belli istekler gonderir.
/// <summary>StopTime liste sinirlarini dogrular.</summary>
public sealed class StopTimeListInputValidator : AbstractValidator<DataTableListInput<StopTimeFilter>>
{
    // Liste isteginin sayfalama sinirlarini belirler.
    public StopTimeListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
