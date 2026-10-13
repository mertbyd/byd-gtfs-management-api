using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.StopTimes;

// islevi: stop_times.txt satirinin temel sekil ve zaman alanlarini dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme istekleri icin ayni public girdi kurallarini kaydeder.
/// <summary>StopTime ortak girdi kurallarini tanimlar.</summary>
public abstract class StopTimeInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : StopTimeInputDto
{
    // Ortak GTFS sekil ve zaman kurallarini her iki istek tipine kaydeder.
    protected StopTimeInputDtoValidatorBase()
    {
        RuleFor(input => input)
            .Must(input => input.StopId is not null || input.LocationGroupId is not null)
            .WithErrorCode(StopTimeExceptionCodes.StopRequired);
        RuleFor(input => input)
            .Must(input => input.StopId is null || input.LocationGroupId is null)
            .WithErrorCode(StopTimeExceptionCodes.StopWithLocationGroupForbidden);
        RuleFor(input => input)
            .Must(input => !string.IsNullOrWhiteSpace(input.StartPickupDropOffWindow)
                && !string.IsNullOrWhiteSpace(input.EndPickupDropOffWindow))
            .When(input => input.LocationGroupId is not null)
            .WithErrorCode(StopTimeExceptionCodes.WindowInvalid);
        RuleFor(input => input.StopSequence)
            .GreaterThanOrEqualTo(0).WithErrorCode(StopTimeExceptionCodes.StopSequenceInvalid);
        RuleFor(input => input.StopHeadsign)
            .MaximumLength(StopTimeConsts.MaxHeadsignLength).WithErrorCode(StopTimeExceptionCodes.HeadsignTooLong);
        RuleFor(input => input.PickupBookingRuleId)
            .MaximumLength(StopTimeConsts.MaxBookingRuleIdLength)
            .WithErrorCode(StopTimeExceptionCodes.BookingRuleIdTooLong);
        RuleFor(input => input.DropOffBookingRuleId)
            .MaximumLength(StopTimeConsts.MaxBookingRuleIdLength)
            .WithErrorCode(StopTimeExceptionCodes.BookingRuleIdTooLong);
        RuleFor(input => input.ShapeDistTraveled)
            .Must(value => value is null || (value >= 0 && !double.IsInfinity(value.Value) && !double.IsNaN(value.Value)))
            .WithErrorCode(StopTimeExceptionCodes.ShapeDistanceInvalid);
        RuleFor(input => input.Timepoint)
            .Must(value => value is null or 0 or 1).WithErrorCode(StopTimeExceptionCodes.TimepointInvalid);
        RuleFor(input => input)
            .Must(input => input.Timepoint != 1 || (!string.IsNullOrWhiteSpace(input.ArrivalTime)
                && !string.IsNullOrWhiteSpace(input.DepartureTime)))
            .WithErrorCode(StopTimeExceptionCodes.TimeRequired);
        RuleFor(input => input.ArrivalTime)
            .Matches(GtfsTimeConsts.FormatPattern).When(input => !string.IsNullOrWhiteSpace(input.ArrivalTime))
            .WithErrorCode(StopTimeExceptionCodes.TimeInvalid);
        RuleFor(input => input.DepartureTime)
            .Matches(GtfsTimeConsts.FormatPattern).When(input => !string.IsNullOrWhiteSpace(input.DepartureTime))
            .WithErrorCode(StopTimeExceptionCodes.TimeInvalid);
        RuleFor(input => input.StartPickupDropOffWindow)
            .Matches(GtfsTimeConsts.FormatPattern).When(input => !string.IsNullOrWhiteSpace(input.StartPickupDropOffWindow))
            .WithErrorCode(StopTimeExceptionCodes.TimeInvalid);
        RuleFor(input => input.EndPickupDropOffWindow)
            .Matches(GtfsTimeConsts.FormatPattern).When(input => !string.IsNullOrWhiteSpace(input.EndPickupDropOffWindow))
            .WithErrorCode(StopTimeExceptionCodes.TimeInvalid);
        RuleFor(input => input)
            .Must(input => string.IsNullOrWhiteSpace(input.StartPickupDropOffWindow)
                == string.IsNullOrWhiteSpace(input.EndPickupDropOffWindow))
            .WithErrorCode(StopTimeExceptionCodes.WindowInvalid);
        RuleFor(input => input)
            .Must(input => string.IsNullOrWhiteSpace(input.ArrivalTime) && string.IsNullOrWhiteSpace(input.DepartureTime))
            .When(input => !string.IsNullOrWhiteSpace(input.StartPickupDropOffWindow)
                || !string.IsNullOrWhiteSpace(input.EndPickupDropOffWindow))
            .WithErrorCode(StopTimeExceptionCodes.TimeForbidden);
    }
}
