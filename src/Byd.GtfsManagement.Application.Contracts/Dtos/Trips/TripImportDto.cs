using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Trips;

// islevi: trips.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Trip ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Trips, GtfsImportOrders.Trip)]
public class TripImportDto
{
    [Name(GtfsColumnNames.TripId), GtfsReference(TripConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.RouteId), GtfsReference(RouteConsts.TableName)]
    public Guid RouteId { get; set; }

    [Name(GtfsColumnNames.ServiceId), GtfsReference(ServiceConsts.TableName)]
    public Guid ServiceId { get; set; }

    [Name(GtfsColumnNames.DirectionId), GtfsReference(TripDirectionConsts.TableName)]
    public int? TripDirectionId { get; set; }

    [Name(GtfsColumnNames.WheelchairAccessible), GtfsReference(WheelchairBoardingConsts.TableName)]
    public int? WheelchairAccessibleId { get; set; }

    [Name(GtfsColumnNames.BikesAllowed), GtfsReference(BikesAllowedConsts.TableName)]
    public int? BikesAllowedId { get; set; }

    [Name(GtfsColumnNames.CarsAllowed), GtfsReference(CarsAllowedConsts.TableName)]
    public int? CarsAllowedId { get; set; }

    [Name(GtfsColumnNames.TripId)]
    public string GtfsTripId { get; set; } = default!;

    [Name(GtfsColumnNames.ShapeId)]
    public string? GtfsShapeId { get; set; }

    [Name(GtfsColumnNames.BlockId)]
    public string? BlockId { get; set; }

    [Name(GtfsColumnNames.TripHeadsign)]
    public string? Headsign { get; set; }

    [Name(GtfsColumnNames.TripShortName)]
    public string? ShortName { get; set; }
}
