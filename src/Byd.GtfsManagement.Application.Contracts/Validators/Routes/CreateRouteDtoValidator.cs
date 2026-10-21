using Byd.GtfsManagement.Dtos.Routes;

namespace Byd.GtfsManagement.Validators.Routes;

// mirastan gelen: routes.txt alanlarinin zorunluluk, uzunluk, renk ve sira kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateRouteDtoValidator : RouteInputDtoValidatorBase<CreateRouteDto>
{
}
