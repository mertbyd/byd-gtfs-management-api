using Byd.GtfsManagement.Dtos.FareMedias;

namespace Byd.GtfsManagement.Validators.FareMedias;

// mirastan gelen: fare_media.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateFareMediaDtoValidator : FareMediaInputDtoValidatorBase<CreateFareMediaDto>
{
}
