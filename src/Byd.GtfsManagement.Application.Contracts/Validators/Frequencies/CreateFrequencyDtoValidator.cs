using FluentValidation;
using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.Frequencies;

// mirastan gelen: ortak frequencies.txt alan kurallari.
// burada yazilan: sefer kimligi zorunlulugu.
// islevi: Frequency olusturma girdisini dogrular.
// sistemdeki gorevi: Yeni araligin sefer bagini zorunlu tutar.
/// <summary>Sefer sikligi olusturma kurallarini tanimlar.</summary>
public sealed class CreateFrequencyDtoValidator : FrequencyInputDtoValidatorBase<CreateFrequencyDto>
{
    // Yeni kaydin sefer bagini zorunlu tutar.
    public CreateFrequencyDtoValidator()
    {
        RuleFor(input => input.TripId).NotEmpty().WithErrorCode(FrequencyExceptionCodes.TripRequired);
    }
}
