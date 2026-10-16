using Byd.GtfsManagement.Dtos.Trips;

namespace Byd.GtfsManagement.Validators.Trips;

// mirastan gelen: trips.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateTripDtoValidator : TripInputDtoValidatorBase<CreateTripDto>
{
}
