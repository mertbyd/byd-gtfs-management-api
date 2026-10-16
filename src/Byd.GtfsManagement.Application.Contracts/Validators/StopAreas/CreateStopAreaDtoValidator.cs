using Byd.GtfsManagement.Dtos.StopAreas;

namespace Byd.GtfsManagement.Validators.StopAreas;

// mirastan gelen: stop_areas.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateStopAreaDtoValidator : StopAreaInputDtoValidatorBase<CreateStopAreaDto>
{
}
