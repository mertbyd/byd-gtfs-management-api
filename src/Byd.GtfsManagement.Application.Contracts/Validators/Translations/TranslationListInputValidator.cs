using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Translations;

namespace Byd.GtfsManagement.Validators.Translations;

// islevi: Translation liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS ceviri liste sinirlarini tanimlar.</summary>
public sealed class TranslationListInputValidator : AbstractValidator<DataTableListInput<TranslationFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public TranslationListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
