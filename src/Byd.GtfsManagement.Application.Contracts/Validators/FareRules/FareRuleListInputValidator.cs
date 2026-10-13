using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.FareRules;

namespace Byd.GtfsManagement.Validators.FareRules;

// islevi: FareRule liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS ucret kurali liste sinirlarini tanimlar.</summary>
public sealed class FareRuleListInputValidator : AbstractValidator<DataTableListInput<FareRuleFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public FareRuleListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
