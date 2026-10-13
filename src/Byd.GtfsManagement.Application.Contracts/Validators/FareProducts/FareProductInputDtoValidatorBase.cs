using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareProducts;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareProducts;

// islevi: FareProduct ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret urunu ortak girdi kurallarini tanimlar.</summary>
public abstract class FareProductInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareProductInputDto
{
    protected FareProductInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsFareProductId)
            .NotEmpty().WithErrorCode(FareProductExceptionCodes.GtfsFareProductIdRequired)
            .MaximumLength(FareProductConsts.MaxGtfsFareProductIdLength).WithErrorCode(FareProductExceptionCodes.GtfsFareProductIdTooLong);
        RuleFor(input => input.FareProductName)
            .MaximumLength(FareProductConsts.MaxFareProductNameLength)
            .When(input => !string.IsNullOrEmpty(input.FareProductName))
            .WithErrorCode(FareProductExceptionCodes.FareProductNameTooLong);
        RuleFor(input => input.GtfsRiderCategoryId)
            .MaximumLength(FareProductConsts.MaxGtfsRiderCategoryIdLength)
            .When(input => !string.IsNullOrEmpty(input.GtfsRiderCategoryId))
            .WithErrorCode(FareProductExceptionCodes.GtfsRiderCategoryIdTooLong);
        RuleFor(input => input.GtfsFareMediaId)
            .MaximumLength(FareProductConsts.MaxGtfsFareMediaIdLength)
            .When(input => !string.IsNullOrEmpty(input.GtfsFareMediaId))
            .WithErrorCode(FareProductExceptionCodes.GtfsFareMediaIdTooLong);
        RuleFor(input => input.Amount)
            .GreaterThanOrEqualTo(0).WithErrorCode(FareProductExceptionCodes.AmountInvalid);
        RuleFor(input => input.Currency)
            .NotEmpty().WithErrorCode(FareProductExceptionCodes.CurrencyRequired)
            .MaximumLength(FareProductConsts.MaxCurrencyLength).WithErrorCode(FareProductExceptionCodes.CurrencyTooLong);
    }
}
