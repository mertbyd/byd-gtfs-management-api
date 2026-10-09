using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Translations;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Translations;

// islevi: translations.txt ortak tablo, alan, dil ve hedef kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ceviri ortak girdi kurallarini tanimlar.</summary>
public abstract class TranslationInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : TranslationInputDto
{
    // Tablo, alan, dil ve hedef kurallarini kaydeder.
    protected TranslationInputDtoValidatorBase()
    {
        RuleFor(input => input.TableName)
            .NotEmpty().WithErrorCode(TranslationExceptionCodes.TableNameRequired)
            .MaximumLength(TranslationConsts.MaxTableNameLength).WithErrorCode(TranslationExceptionCodes.TableNameTooLong);

        RuleFor(input => input.FieldName)
            .NotEmpty().WithErrorCode(TranslationExceptionCodes.FieldNameRequired)
            .MaximumLength(TranslationConsts.MaxFieldNameLength).WithErrorCode(TranslationExceptionCodes.FieldNameTooLong);

        RuleFor(input => input.Language)
            .NotEmpty().WithErrorCode(TranslationExceptionCodes.LanguageRequired)
            .MaximumLength(TranslationConsts.MaxLanguageLength).WithErrorCode(TranslationExceptionCodes.LanguageTooLong);

        RuleFor(input => input.TranslationText)
            .NotEmpty().WithErrorCode(TranslationExceptionCodes.TranslationRequired)
            .MaximumLength(TranslationConsts.MaxTranslationTextLength).WithErrorCode(TranslationExceptionCodes.TranslationTooLong);

        RuleFor(input => input)
            .Must(input => !string.IsNullOrWhiteSpace(input.RecordId) || !string.IsNullOrWhiteSpace(input.FieldValue))
            .WithErrorCode(TranslationExceptionCodes.TargetRequired);

        RuleFor(input => input.RecordId)
            .MaximumLength(TranslationConsts.MaxRecordIdLength)
            .When(input => !string.IsNullOrEmpty(input.RecordId))
            .WithErrorCode(TranslationExceptionCodes.RecordIdTooLong);

        RuleFor(input => input.RecordSubId)
            .MaximumLength(TranslationConsts.MaxRecordSubIdLength)
            .When(input => !string.IsNullOrEmpty(input.RecordSubId))
            .WithErrorCode(TranslationExceptionCodes.RecordSubIdTooLong);

        RuleFor(input => input.FieldValue)
            .MaximumLength(TranslationConsts.MaxFieldValueLength)
            .When(input => !string.IsNullOrEmpty(input.FieldValue))
            .WithErrorCode(TranslationExceptionCodes.FieldValueTooLong);
    }
}
