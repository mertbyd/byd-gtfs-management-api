using FluentValidation;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.StopTimes;

// mirastan gelen: stop_times.txt ortak sekil kurallari.
// burada yazilan: sefer kimligi zorunlulugu.
// islevi: StopTime olusturma kurallarini tanimlar.
// sistemdeki gorevi: Zorunlu sefer bagini ortak dogrulamaya ekler.
/// <summary>StopTime olusturma kurallarini tanimlar.</summary>
public sealed class CreateStopTimeDtoValidator : StopTimeInputDtoValidatorBase<CreateStopTimeDto>
{
    // Yeni kayit icin sefer bagini zorunlu tutar.
    public CreateStopTimeDtoValidator()
    {
        RuleFor(input => input.TripId).NotEmpty().WithErrorCode(StopTimeExceptionCodes.TripRequired);
    }
}
