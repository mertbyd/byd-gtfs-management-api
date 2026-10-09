using System;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: routes.txt kaydinin tum GTFS alanlarini ve bagli kayitlarin adlarini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class RouteDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid? AgencyId { get; set; }
    public string? AgencyName { get; set; }
    public int RouteTypeId { get; set; }
    public string RouteTypeName { get; set; } = default!;
    public int? ContinuousPickupId { get; set; }
    public string? ContinuousPickupName { get; set; }
    public int? ContinuousDropOffId { get; set; }
    public string? ContinuousDropOffName { get; set; }
    public Guid? NetworkId { get; set; }
    public string? NetworkName { get; set; }
    public int? CemvSupportId { get; set; }
    public string? CemvSupportName { get; set; }
    public string GtfsRouteId { get; set; } = default!;
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? Color { get; set; }
    public string? TextColor { get; set; }
    public int? SortOrder { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
