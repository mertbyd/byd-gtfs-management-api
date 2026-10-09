using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.FareAttributes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.FareAttributes;

// islevi: fare_attributes.txt ortak ucret, fiyat, para birimi ve aktarma kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS ucret ozelligi ortak girdi kurallarini tanimlar.</summary>
public abstract class FareAttributeInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : FareAttributeInputDto
{
    // Ucret kimligi, fiyat, para birimi, odeme sekli ve aktarma kurallarini kaydeder.
    protected FareAttributeInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsFareId)
            .NotEmpty().WithErrorCode(FareAttributeExceptionCodes.FareRequired)
            .MaximumLength(FareAttributeConsts.MaxGtfsFareIdLength).WithErrorCode(FareAttributeExceptionCodes.FareTooLong);

        RuleFor(input => input.Price)
            .GreaterThanOrEqualTo(0).WithErrorCode(FareAttributeExceptionCodes.PriceInvalid);

        RuleFor(input => input.CurrencyType)
            .NotEmpty().WithErrorCode(FareAttributeExceptionCodes.CurrencyTypeRequired)
            .Length(FareAttributeConsts.MaxCurrencyTypeLength).WithErrorCode(FareAttributeExceptionCodes.CurrencyTypeInvalid);

        RuleFor(input => input.PaymentMethod)
            .Must(val => val is FareAttributeConsts.PaymentMethodPaidOnBoard or FareAttributeConsts.PaymentMethodPaidBeforeBoarding)
            .WithErrorCode(FareAttributeExceptionCodes.PaymentMethodInvalid);

        RuleFor(input => input.Transfers)
            .Must(val => !val.HasValue || val.Value is FareAttributeConsts.TransfersNone or FareAttributeConsts.TransfersOnce or FareAttributeConsts.TransfersTwice)
            .WithErrorCode(FareAttributeExceptionCodes.TransfersInvalid);

        RuleFor(input => input.TransferDuration)
            .GreaterThanOrEqualTo(0)
            .When(input => input.TransferDuration.HasValue)
            .WithErrorCode(FareAttributeExceptionCodes.TransferDurationInvalid);
    }
}
