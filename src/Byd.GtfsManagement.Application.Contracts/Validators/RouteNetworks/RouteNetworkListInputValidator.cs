using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.RouteNetworks;

namespace Byd.GtfsManagement.Validators.RouteNetworks;

// islevi: RouteNetwork liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS hat ag iliskisi liste sinirlarini tanimlar.</summary>
public sealed class RouteNetworkListInputValidator : AbstractValidator<DataTableListInput<RouteNetworkFilter>>
{
    public RouteNetworkListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
