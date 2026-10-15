using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Pathways;

// islevi: pathways.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Pathway ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Pathways, GtfsImportOrders.Pathway)]
public class PathwayImportDto
{
    [Name(GtfsColumnNames.FromStopId), GtfsReference(StopConsts.TableName)]
    public Guid FromStopId { get; set; }

    [Name(GtfsColumnNames.ToStopId), GtfsReference(StopConsts.TableName)]
    public Guid ToStopId { get; set; }

    [Name(GtfsColumnNames.PathwayId)]
    public string GtfsPathwayId { get; set; } = default!;

    [Name(GtfsColumnNames.PathwayMode)]
    public int PathwayMode { get; set; }

    [Name(GtfsColumnNames.IsBidirectional)]
    public int IsBidirectional { get; set; }

    [Name(GtfsColumnNames.Length)]
    public double? Length { get; set; }

    [Name(GtfsColumnNames.TraversalTime)]
    public int? TraversalTime { get; set; }

    [Name(GtfsColumnNames.StairCount)]
    public int? StairCount { get; set; }

    [Name(GtfsColumnNames.MaxSlope)]
    public double? MaxSlope { get; set; }

    [Name(GtfsColumnNames.MinWidth)]
    public double? MinWidth { get; set; }

    [Name(GtfsColumnNames.SignpostedAs)]
    public string? SignpostedAs { get; set; }

    [Name(GtfsColumnNames.ReversedSignpostedAs)]
    public string? ReversedSignpostedAs { get; set; }
}
