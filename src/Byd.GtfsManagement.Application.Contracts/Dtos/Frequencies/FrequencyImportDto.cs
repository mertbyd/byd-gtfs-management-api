using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Frequencies;

// islevi: frequencies.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Frequency ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Frequencies, GtfsImportOrders.Frequency)]
public class FrequencyImportDto
{
    [Name(GtfsColumnNames.TripId), GtfsReference(TripConsts.TableName)]
    public Guid TripId { get; set; }

    [Name(GtfsColumnNames.StartTime)]
    public string StartTime { get; set; } = default!;

    [Name(GtfsColumnNames.EndTime)]
    public string EndTime { get; set; } = default!;

    [Name(GtfsColumnNames.HeadwaySecs)]
    public int HeadwaySecs { get; set; }

    [Name(GtfsColumnNames.ExactTimes)]
    public int? ExactTimes { get; set; }
}
