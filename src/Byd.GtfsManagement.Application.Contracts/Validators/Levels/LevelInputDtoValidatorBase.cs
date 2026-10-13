using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Levels;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Levels;

// islevi: levels.txt ortak kat ve zorunlu alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS kat ortak girdi kurallarini tanimlar.</summary>
public abstract class LevelInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : LevelInputDto
{
    // Kat kimligi, sayisal dizin ve ad kurallarini kaydeder.
    protected LevelInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsLevelId)
            .NotEmpty().WithErrorCode(LevelExceptionCodes.LevelRequired)
            .MaximumLength(LevelConsts.MaxGtfsLevelIdLength).WithErrorCode(LevelExceptionCodes.LevelTooLong);

        RuleFor(input => input.LevelIndex)
            .Must(index => !double.IsNaN(index) && !double.IsInfinity(index))
            .WithErrorCode(LevelExceptionCodes.IndexInvalid);

        RuleFor(input => input.Name)
            .MaximumLength(LevelConsts.MaxNameLength)
            .When(input => !string.IsNullOrEmpty(input.Name))
            .WithErrorCode(LevelExceptionCodes.NameTooLong);
    }
}
