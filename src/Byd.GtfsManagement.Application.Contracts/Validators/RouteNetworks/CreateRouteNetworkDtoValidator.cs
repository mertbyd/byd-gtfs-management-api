using FluentValidation;
using Byd.GtfsManagement.Dtos.RouteNetworks;
using Byd.GtfsManagement.ExceptionCodes;

namespace Byd.GtfsManagement.Validators.RouteNetworks;

// mirastan gelen: route_networks.txt alanlarinin zorunluluk ve uzunluk kurallari.
// burada yazilan: yalnizca kaydin yazilacagi Feed'in zorunlulugu.
public sealed class CreateRouteNetworkDtoValidator : RouteNetworkInputDtoValidatorBase<CreateRouteNetworkDto>
{
    // Hedef Feed'in istekte bulunmasini kaydeder.
    public CreateRouteNetworkDtoValidator()
    {
        RuleFor(input => input.FeedId)
            .NotEmpty().WithErrorCode(RouteNetworkExceptionCodes.FeedRequired);
    }
}
