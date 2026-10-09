using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.LocationGroups;

namespace Byd.GtfsManagement.Validators.LocationGroups;

// islevi: LocationGroup liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS konum grubu liste sinirlarini tanimlar.</summary>
public sealed class LocationGroupListInputValidator : AbstractValidator<DataTableListInput<LocationGroupFilter>>
{
    public LocationGroupListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
