using System;

namespace Byd.GtfsManagement.Models.RouteNetworks;

// islevi: Yeni RouteNetwork kaydi icin gerekli Feed ve alanlari tasir.
// sistemdeki gorevi: Manager.CreateAsync isleminin model sozlesmesini olusturur.
/// <summary>GTFS hat ag iliskisi olusturma domain modelini tasir.</summary>
public class RouteNetworkCreateModel : RouteNetworkInputModel
{
    public Guid FeedId { get; set; }
}
