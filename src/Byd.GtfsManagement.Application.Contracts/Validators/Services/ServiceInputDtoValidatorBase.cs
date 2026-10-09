using FluentValidation;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Services;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Services;

// islevi: GTFS service_id alaninin zorunluluk ve uzunluk kurallarini tek yerde dogrular.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ayni sekil sozlesmesinden gecmesini saglar.
public abstract class ServiceInputDtoValidatorBase<TDto> : AbstractValidator<TDto>
    where TDto : ServiceInputDto
{
    // Servis kimliginin sekil kurallarini kaydeder.
    protected ServiceInputDtoValidatorBase()
    {
        RuleFor(input => input.GtfsServiceId)
            .NotEmpty().WithErrorCode(ServiceExceptionCodes.GtfsServiceIdRequired)
            .MaximumLength(ServiceConsts.MaxGtfsServiceIdLength)
            .WithErrorCode(ServiceExceptionCodes.GtfsServiceIdTooLong);
    }
}
