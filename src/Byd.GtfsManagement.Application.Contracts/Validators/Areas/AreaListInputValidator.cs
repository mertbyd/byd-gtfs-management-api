using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Areas;

namespace Byd.GtfsManagement.Validators.Areas;

// islevi: Area liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS bolge liste sinirlarini tanimlar.</summary>
public sealed class AreaListInputValidator : AbstractValidator<DataTableListInput<AreaFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public AreaListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
