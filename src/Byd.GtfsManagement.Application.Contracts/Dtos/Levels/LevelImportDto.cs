using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: levels.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Level ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Levels, GtfsImportOrders.Level)]
public class LevelImportDto
{
    [Name(GtfsColumnNames.LevelId), GtfsReference(LevelConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.LevelId)]
    public string GtfsLevelId { get; set; } = default!;

    [Name(GtfsColumnNames.LevelIndex)]
    public double LevelIndex { get; set; }

    [Name(GtfsColumnNames.LevelName)]
    public string? Name { get; set; }
}
