using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Pathways;

namespace Byd.GtfsManagement.Validators.Pathways;

// islevi: Pathway liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS yuruyus yolu liste sinirlarini tanimlar.</summary>
public sealed class PathwayListInputValidator : AbstractValidator<DataTableListInput<PathwayFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public PathwayListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
