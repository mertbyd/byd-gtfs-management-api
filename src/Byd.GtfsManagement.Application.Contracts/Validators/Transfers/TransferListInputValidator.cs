using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Transfers;

namespace Byd.GtfsManagement.Validators.Transfers;

// islevi: Transfer liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS aktarma liste sinirlarini tanimlar.</summary>
public sealed class TransferListInputValidator : AbstractValidator<DataTableListInput<TransferFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public TransferListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
