using System;

namespace Byd.GtfsManagement.Models.Routes;

// islevi: routes.txt kaydinin duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncellemenin ayni normalizasyon yolundan gecmesini saglar.
public abstract class RouteInputModel
{
    public Guid? AgencyId { get; set; }
    public int RouteTypeId { get; set; }
    public int? ContinuousPickupId { get; set; }
    public int? ContinuousDropOffId { get; set; }
    public Guid? NetworkId { get; set; }
    public int? CemvSupportId { get; set; }
    public string GtfsRouteId { get; set; } = default!;
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Color { get; set; }
    public string? TextColor { get; set; }
    public int? SortOrder { get; set; }
}
