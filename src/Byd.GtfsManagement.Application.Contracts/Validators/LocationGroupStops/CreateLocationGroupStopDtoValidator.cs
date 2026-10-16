using Byd.GtfsManagement.Dtos.LocationGroupStops;

namespace Byd.GtfsManagement.Validators.LocationGroupStops;

// mirastan gelen: location_group_stops.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateLocationGroupStopDtoValidator : LocationGroupStopInputDtoValidatorBase<CreateLocationGroupStopDto>
{
}
