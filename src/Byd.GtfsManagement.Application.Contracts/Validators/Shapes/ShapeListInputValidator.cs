using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Shapes;

namespace Byd.GtfsManagement.Validators.Shapes;

// islevi: Shape liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS sekil liste sinirlarini tanimlar.</summary>
public sealed class ShapeListInputValidator : AbstractValidator<DataTableListInput<ShapeFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public ShapeListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
