using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Trips;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Trips;

// islevi: trips.txt alanlarinin zorunluluk ve uzunluk kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class TripInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : TripInputDto
{
    // GTFS sefer alanlarinin sekil kurallarini kaydeder.
    protected TripInputDtoValidatorBase()
    {
        RuleFor(input => input.RouteId)
            .NotEmpty().WithErrorCode(TripExceptionCodes.RouteRequired);
        RuleFor(input => input.ServiceId)
            .NotEmpty().WithErrorCode(TripExceptionCodes.ServiceRequired);
        RuleFor(input => input.GtfsTripId)
            .NotEmpty().WithErrorCode(TripExceptionCodes.GtfsTripIdRequired)
            .MaximumLength(TripConsts.MaxGtfsTripIdLength).WithErrorCode(TripExceptionCodes.GtfsTripIdTooLong);
        RuleFor(input => input.GtfsShapeId)
            .MaximumLength(ShapeConsts.MaxGtfsShapeIdLength).WithErrorCode(TripExceptionCodes.GtfsShapeIdTooLong);
        RuleFor(input => input.Headsign)
            .MaximumLength(TripConsts.MaxHeadsignLength).WithErrorCode(TripExceptionCodes.HeadsignTooLong);
        RuleFor(input => input.ShortName)
            .MaximumLength(TripConsts.MaxShortNameLength).WithErrorCode(TripExceptionCodes.ShortNameTooLong);
        RuleFor(input => input.BlockId)
            .MaximumLength(TripConsts.MaxBlockIdLength).WithErrorCode(TripExceptionCodes.BlockIdTooLong);
    }
}
