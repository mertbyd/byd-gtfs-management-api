using FluentValidation;
using Byd.CoreExtensions.ExceptionCodes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Levels;

namespace Byd.GtfsManagement.Validators.Levels;

// islevi: Level liste sayfalama sinirlarini dogrular.
// sistemdeki gorevi: Dinamik sorgu motoruna sinirli istek gonderir.
/// <summary>GTFS kat liste sinirlarini tanimlar.</summary>
public sealed class LevelListInputValidator : AbstractValidator<DataTableListInput<LevelFilter>>
{
    // Liste isteginin sayfa ve sayfa boyutu araligini belirler.
    public LevelListInputValidator()
    {
        RuleFor(input => input.Page)
            .GreaterThanOrEqualTo(DataTablePaging.FirstPage)
            .WithErrorCode(GeneralExceptionCodes.InvalidPage);
        RuleFor(input => input.PageSize)
            .InclusiveBetween(1, DataTablePaging.MaxPageSize)
            .WithErrorCode(GeneralExceptionCodes.InvalidPageSize);
    }
}
