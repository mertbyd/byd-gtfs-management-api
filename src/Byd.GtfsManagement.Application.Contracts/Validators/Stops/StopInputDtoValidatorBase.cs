using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Stops;

// islevi: stops.txt alanlarinin zorunluluk, uzunluk ve koordinat araligi kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class StopInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : StopInputDto
{
    // GTFS durak alanlarinin sekil kurallarini kaydeder.
    protected StopInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsStopId)
            .NotEmpty().WithErrorCode(StopExceptionCodes.GtfsStopIdRequired)
            .MaximumLength(StopConsts.MaxGtfsStopIdLength).WithErrorCode(StopExceptionCodes.GtfsStopIdTooLong);
        RuleFor(input => input.Code)
            .MaximumLength(StopConsts.MaxCodeLength).WithErrorCode(StopExceptionCodes.CodeTooLong);
        RuleFor(input => input.Name)
            .MaximumLength(StopConsts.MaxNameLength).WithErrorCode(StopExceptionCodes.NameTooLong);
        RuleFor(input => input.TtsName)
            .MaximumLength(StopConsts.MaxTtsNameLength).WithErrorCode(StopExceptionCodes.TtsNameTooLong);
        RuleFor(input => input.Description)
            .MaximumLength(StopConsts.MaxDescriptionLength).WithErrorCode(StopExceptionCodes.DescriptionTooLong);
        RuleFor(input => input.Latitude)
            .InclusiveBetween(StopConsts.MinLatitude, StopConsts.MaxLatitude)
            .WithErrorCode(StopExceptionCodes.LatitudeOutOfRange)
            .When(input => input.Latitude is not null);
        RuleFor(input => input.Longitude)
            .InclusiveBetween(StopConsts.MinLongitude, StopConsts.MaxLongitude)
            .WithErrorCode(StopExceptionCodes.LongitudeOutOfRange)
            .When(input => input.Longitude is not null);
        RuleFor(input => input.ZoneId)
            .MaximumLength(StopConsts.MaxZoneIdLength).WithErrorCode(StopExceptionCodes.ZoneIdTooLong);
        RuleFor(input => input.Url)
            .MaximumLength(StopConsts.MaxUrlLength).WithErrorCode(StopExceptionCodes.UrlTooLong);
        RuleFor(input => input.Timezone)
            .MaximumLength(StopConsts.MaxTimezoneLength).WithErrorCode(StopExceptionCodes.TimezoneTooLong);
        RuleFor(input => input.PlatformCode)
            .MaximumLength(StopConsts.MaxPlatformCodeLength).WithErrorCode(StopExceptionCodes.PlatformCodeTooLong);
    }
}
