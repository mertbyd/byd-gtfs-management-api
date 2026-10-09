using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.RiderCategories;

namespace Byd.GtfsManagement.Validators.RiderCategories;

// islevi: RiderCategory liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS yolcu kategorisi liste sinirlarini tanimlar.</summary>
public sealed class RiderCategoryListInputValidator : AbstractValidator<DataTableListInput<RiderCategoryFilter>>
{
    public RiderCategoryListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
