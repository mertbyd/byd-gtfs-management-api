using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.StopAreas;

namespace Byd.GtfsManagement.Validators.StopAreas;

// islevi: StopArea liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS durak bolge iliskisi liste sinirlarini tanimlar.</summary>
public sealed class StopAreaListInputValidator : AbstractValidator<DataTableListInput<StopAreaFilter>>
{
    public StopAreaListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
