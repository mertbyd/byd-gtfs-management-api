using Byd.GtfsManagement.Dtos.RouteNetworks;

namespace Byd.GtfsManagement.Validators.RouteNetworks;

// mirastan gelen: route_networks.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yok; Feed istekten alinmaz, olusturma ek alan tasimaz.
public sealed class CreateRouteNetworkDtoValidator : RouteNetworkInputDtoValidatorBase<CreateRouteNetworkDto>
{
}
