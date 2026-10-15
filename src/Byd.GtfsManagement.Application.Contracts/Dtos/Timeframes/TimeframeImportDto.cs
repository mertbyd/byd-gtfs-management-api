using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: timeframes.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Timeframe ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Timeframes, GtfsImportOrders.Timeframe)]
public class TimeframeImportDto
{
    [Name(GtfsColumnNames.TimeframeGroupId)]
    public string TimeframeGroupId { get; set; } = default!;
    [Name(GtfsColumnNames.ServiceId)]
    public string GtfsServiceId { get; set; } = default!;

    [Name(GtfsColumnNames.StartTime)]
    public string? StartTime { get; set; }

    [Name(GtfsColumnNames.EndTime)]
    public string? EndTime { get; set; }

}
