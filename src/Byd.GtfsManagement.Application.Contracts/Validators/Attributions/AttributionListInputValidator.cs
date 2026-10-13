using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Attributions;

namespace Byd.GtfsManagement.Validators.Attributions;

// islevi: Attribution liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS atif liste sinirlarini tanimlar.</summary>
public sealed class AttributionListInputValidator : AbstractValidator<DataTableListInput<AttributionFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public AttributionListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
