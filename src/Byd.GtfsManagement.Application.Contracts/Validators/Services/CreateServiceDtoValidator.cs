using Byd.GtfsManagement.Dtos.Services;

namespace Byd.GtfsManagement.Validators.Services;

// mirastan gelen: GtfsServiceId zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateServiceDtoValidator : ServiceInputDtoValidatorBase<CreateServiceDto>
{
}
