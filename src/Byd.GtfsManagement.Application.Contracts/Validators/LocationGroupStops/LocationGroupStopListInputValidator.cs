using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.LocationGroupStops;

namespace Byd.GtfsManagement.Validators.LocationGroupStops;

// islevi: LocationGroupStop liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS konum grubu durak iliskisi liste sinirlarini tanimlar.</summary>
public sealed class LocationGroupStopListInputValidator : AbstractValidator<DataTableListInput<LocationGroupStopFilter>>
{
    public LocationGroupStopListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
