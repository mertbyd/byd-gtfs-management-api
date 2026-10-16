using Byd.GtfsManagement.Dtos.Timeframes;

namespace Byd.GtfsManagement.Validators.Timeframes;

// mirastan gelen: timeframes.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateTimeframeDtoValidator : TimeframeInputDtoValidatorBase<CreateTimeframeDto>
{
}
