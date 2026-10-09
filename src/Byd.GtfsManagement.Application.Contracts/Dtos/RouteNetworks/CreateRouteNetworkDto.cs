using System;

namespace Byd.GtfsManagement.Dtos.RouteNetworks;

// islevi: Yeni hat ag iliskisi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS hat ag iliskisi olusturma istegini tasir.</summary>
public sealed class CreateRouteNetworkDto : RouteNetworkInputDto
{
    public Guid FeedId { get; set; }
}
