using Byd.GtfsManagement.Dtos.Agencies;

namespace Byd.GtfsManagement.Validators.Agencies;

// mirastan gelen: agency.txt alanlarinin zorunluluk, uzunluk ve format kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateAgencyDtoValidator : AgencyInputDtoValidatorBase<CreateAgencyDto>
{
}
