using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.FareAttributes;

namespace Byd.GtfsManagement.Validators.FareAttributes;

// islevi: FareAttribute liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS ucret ozelligi liste sinirlarini tanimlar.</summary>
public sealed class FareAttributeListInputValidator : AbstractValidator<DataTableListInput<FareAttributeFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public FareAttributeListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
