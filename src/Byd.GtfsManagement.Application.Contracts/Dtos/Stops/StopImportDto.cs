using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: stops.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Stop ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Stops, GtfsImportOrders.Stop)]
public class StopImportDto
{
    [Name(GtfsColumnNames.StopId), GtfsReference(StopConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.LocationType), GtfsReference(LocationTypeConsts.TableName)]
    public int? LocationTypeId { get; set; }

    [Name(GtfsColumnNames.WheelchairBoarding), GtfsReference(WheelchairBoardingConsts.TableName)]
    public int? WheelchairBoardingId { get; set; }

    [Name(GtfsColumnNames.ParentStation), GtfsReference(StopConsts.TableName)]
    public Guid? ParentStationId { get; set; }

    [Name(GtfsColumnNames.LevelId), GtfsReference(LevelConsts.TableName)]
    public Guid? LevelId { get; set; }

    [Name(GtfsColumnNames.StopAccess), GtfsReference(StopAccessConsts.TableName)]
    public int? StopAccessId { get; set; }

    [Name(GtfsColumnNames.StopId)]
    public string GtfsStopId { get; set; } = default!;

    [Name(GtfsColumnNames.ZoneId)]
    public string? ZoneId { get; set; }

    [Name(GtfsColumnNames.StopCode)]
    public string? Code { get; set; }

    [Name(GtfsColumnNames.StopName)]
    public string? Name { get; set; }

    [Name(GtfsColumnNames.TtsStopName)]
    public string? TtsName { get; set; }

    [Name(GtfsColumnNames.StopDesc)]
    public string? Description { get; set; }

    [Name(GtfsColumnNames.StopLat)]
    public double? Latitude { get; set; }

    [Name(GtfsColumnNames.StopLon)]
    public double? Longitude { get; set; }

    [Name(GtfsColumnNames.StopUrl)]
    public string? Url { get; set; }

    [Name(GtfsColumnNames.StopTimezone)]
    public string? Timezone { get; set; }

    [Name(GtfsColumnNames.PlatformCode)]
    public string? PlatformCode { get; set; }
}
