using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: routes.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Route ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Routes, GtfsImportOrders.Route)]
public class RouteImportDto
{
    [Name(GtfsColumnNames.RouteId), GtfsReference(RouteConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.AgencyId), GtfsReference(AgencyConsts.TableName)]
    public Guid? AgencyId { get; set; }

    [Name(GtfsColumnNames.RouteType), GtfsReference(RouteTypeConsts.TableName)]
    public int RouteTypeId { get; set; }

    [Name(GtfsColumnNames.ContinuousPickup), GtfsReference(ContinuousPickupDropOffConsts.TableName)]
    public int? ContinuousPickupId { get; set; }

    [Name(GtfsColumnNames.ContinuousDropOff), GtfsReference(ContinuousPickupDropOffConsts.TableName)]
    public int? ContinuousDropOffId { get; set; }

    [Name(GtfsColumnNames.NetworkId), GtfsReference(NetworkConsts.TableName)]
    public Guid? NetworkId { get; set; }

    [Name(GtfsColumnNames.CemvSupport), GtfsReference(CemvSupportConsts.TableName)]
    public int? CemvSupportId { get; set; }

    [Name(GtfsColumnNames.RouteId)]
    public string GtfsRouteId { get; set; } = default!;

    [Name(GtfsColumnNames.RouteShortName)]
    public string? ShortName { get; set; }

    [Name(GtfsColumnNames.RouteLongName)]
    public string? LongName { get; set; }

    [Name(GtfsColumnNames.RouteDesc)]
    public string? Description { get; set; }

    [Name(GtfsColumnNames.RouteUrl)]
    public string? Url { get; set; }

    [Name(GtfsColumnNames.RouteColor)]
    public string? Color { get; set; }

    [Name(GtfsColumnNames.RouteTextColor)]
    public string? TextColor { get; set; }

    [Name(GtfsColumnNames.RouteSortOrder)]
    public int? SortOrder { get; set; }
}
