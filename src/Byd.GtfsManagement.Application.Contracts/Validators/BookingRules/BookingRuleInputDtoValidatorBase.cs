using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.BookingRules;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.BookingRules;

// islevi: BookingRule ortak alan kurallarini tanimlar.
// sistemdeki gorevi: Create ve Update isteklerinin ayni public dogrulamayi kullanmasini saglar.
/// <summary>GTFS rezervasyon kurali ortak girdi kurallarini tanimlar.</summary>
public abstract class BookingRuleInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : BookingRuleInputDto
{
    protected BookingRuleInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsBookingRuleId)
            .NotEmpty().WithErrorCode(BookingRuleExceptionCodes.GtfsBookingRuleIdRequired)
            .MaximumLength(BookingRuleConsts.MaxGtfsBookingRuleIdLength).WithErrorCode(BookingRuleExceptionCodes.GtfsBookingRuleIdTooLong);
        RuleFor(input => input.BookingType)
            .NotNull().WithErrorCode(BookingRuleExceptionCodes.BookingTypeRequired);
        RuleFor(input => input.PriorNoticeLastTime)
            .MaximumLength(BookingRuleConsts.MaxPriorNoticeLastTimeLength)
            .When(input => !string.IsNullOrEmpty(input.PriorNoticeLastTime))
            .WithErrorCode(BookingRuleExceptionCodes.PriorNoticeLastTimeTooLong);
        RuleFor(input => input.PriorNoticeStartTime)
            .MaximumLength(BookingRuleConsts.MaxPriorNoticeStartTimeLength)
            .When(input => !string.IsNullOrEmpty(input.PriorNoticeStartTime))
            .WithErrorCode(BookingRuleExceptionCodes.PriorNoticeStartTimeTooLong);
        RuleFor(input => input.PriorNoticeServiceId)
            .MaximumLength(BookingRuleConsts.MaxPriorNoticeServiceIdLength)
            .When(input => !string.IsNullOrEmpty(input.PriorNoticeServiceId))
            .WithErrorCode(BookingRuleExceptionCodes.PriorNoticeServiceIdTooLong);
        RuleFor(input => input.Message)
            .MaximumLength(BookingRuleConsts.MaxMessageLength)
            .When(input => !string.IsNullOrEmpty(input.Message))
            .WithErrorCode(BookingRuleExceptionCodes.MessageTooLong);
        RuleFor(input => input.PickupMessage)
            .MaximumLength(BookingRuleConsts.MaxPickupMessageLength)
            .When(input => !string.IsNullOrEmpty(input.PickupMessage))
            .WithErrorCode(BookingRuleExceptionCodes.PickupMessageTooLong);
        RuleFor(input => input.DropOffMessage)
            .MaximumLength(BookingRuleConsts.MaxDropOffMessageLength)
            .When(input => !string.IsNullOrEmpty(input.DropOffMessage))
            .WithErrorCode(BookingRuleExceptionCodes.DropOffMessageTooLong);
        RuleFor(input => input.PhoneNumber)
            .MaximumLength(BookingRuleConsts.MaxPhoneNumberLength)
            .When(input => !string.IsNullOrEmpty(input.PhoneNumber))
            .WithErrorCode(BookingRuleExceptionCodes.PhoneNumberTooLong);
        RuleFor(input => input.InfoUrl)
            .MaximumLength(BookingRuleConsts.MaxInfoUrlLength)
            .When(input => !string.IsNullOrEmpty(input.InfoUrl))
            .WithErrorCode(BookingRuleExceptionCodes.InfoUrlTooLong);
        RuleFor(input => input.BookingUrl)
            .MaximumLength(BookingRuleConsts.MaxBookingUrlLength)
            .When(input => !string.IsNullOrEmpty(input.BookingUrl))
            .WithErrorCode(BookingRuleExceptionCodes.BookingUrlTooLong);
    }
}
