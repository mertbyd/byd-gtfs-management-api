using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.FareTransferRules;

namespace Byd.GtfsManagement.Validators.FareTransferRules;

// islevi: FareTransferRule liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS ucret aktarma kurali liste sinirlarini tanimlar.</summary>
public sealed class FareTransferRuleListInputValidator : AbstractValidator<DataTableListInput<FareTransferRuleFilter>>
{
    public FareTransferRuleListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
