using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.StopTimes;

// islevi: stop_times.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: StopTime ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.StopTimes, GtfsImportOrders.StopTime)]
public class StopTimeImportDto
{
    [Name(GtfsColumnNames.TripId), GtfsReference(TripConsts.TableName)]
    public Guid TripId { get; set; }

    [Name(GtfsColumnNames.StopId), GtfsReference(StopConsts.TableName)]
    public Guid? StopId { get; set; }

    [Name(GtfsColumnNames.LocationGroupId), GtfsReference(LocationGroupConsts.TableName)]
    public Guid? LocationGroupId { get; set; }

    [Name(GtfsColumnNames.PickupType), GtfsReference(PickupDropOffTypeConsts.TableName)]
    public int? PickupTypeId { get; set; }

    [Name(GtfsColumnNames.DropOffType), GtfsReference(PickupDropOffTypeConsts.TableName)]
    public int? DropOffTypeId { get; set; }

    [Name(GtfsColumnNames.ContinuousPickup), GtfsReference(ContinuousPickupDropOffConsts.TableName)]
    public int? ContinuousPickupId { get; set; }

    [Name(GtfsColumnNames.ContinuousDropOff), GtfsReference(ContinuousPickupDropOffConsts.TableName)]
    public int? ContinuousDropOffId { get; set; }

    [Name(GtfsColumnNames.PickupBookingRuleId)]
    public string? PickupBookingRuleId { get; set; }

    [Name(GtfsColumnNames.DropOffBookingRuleId)]
    public string? DropOffBookingRuleId { get; set; }

    [Name(GtfsColumnNames.StopSequence)]
    public int StopSequence { get; set; }

    [Name(GtfsColumnNames.ArrivalTime)]
    public string? ArrivalTime { get; set; }

    [Name(GtfsColumnNames.DepartureTime)]
    public string? DepartureTime { get; set; }

    [Name(GtfsColumnNames.StopHeadsign)]
    public string? StopHeadsign { get; set; }

    [Name(GtfsColumnNames.StartPickupDropOffWindow)]
    public string? StartPickupDropOffWindow { get; set; }

    [Name(GtfsColumnNames.EndPickupDropOffWindow)]
    public string? EndPickupDropOffWindow { get; set; }

    [Name(GtfsColumnNames.ShapeDistTraveled)]
    public double? ShapeDistTraveled { get; set; }

    [Name(GtfsColumnNames.Timepoint)]
    public int? Timepoint { get; set; }
}
