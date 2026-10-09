using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Frequencies;

namespace Byd.GtfsManagement.Validators.Frequencies;

// islevi: Frequency liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>Sefer sikligi liste sinirlarini dogrular.</summary>
public sealed class FrequencyListInputValidator : AbstractValidator<DataTableListInput<FrequencyFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public FrequencyListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
