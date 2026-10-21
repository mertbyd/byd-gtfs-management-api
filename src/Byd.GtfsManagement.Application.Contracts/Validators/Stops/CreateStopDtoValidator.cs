using Byd.GtfsManagement.Dtos.Stops;

namespace Byd.GtfsManagement.Validators.Stops;

// mirastan gelen: stops.txt alanlarinin zorunluluk, uzunluk ve koordinat araligi kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateStopDtoValidator : StopInputDtoValidatorBase<CreateStopDto>
{
}
