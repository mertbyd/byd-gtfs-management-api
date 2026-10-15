using System;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: routes.txt kaydinin tum GTFS alanlarini ve bagli kayitlarin adlarini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
public class RouteDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid FeedId { get; set; }
    public Guid? AgencyId { get; set; }
    public int RouteTypeId { get; set; }
    public int? ContinuousPickupId { get; set; }
    public int? ContinuousDropOffId { get; set; }
    public Guid? NetworkId { get; set; }
    public int? CemvSupportId { get; set; }

    // Table Columns / Data
    public string FeedName { get; set; } = default!;
    public string? AgencyName { get; set; }
    public string RouteTypeName { get; set; } = default!;
    public string? ContinuousPickupName { get; set; }
    public string? ContinuousDropOffName { get; set; }
    public string? NetworkName { get; set; }
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
