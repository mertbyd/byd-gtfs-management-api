using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.StopAreas;

// islevi: stop_areas.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: StopArea ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.StopAreas, GtfsImportOrders.StopArea)]
public class StopAreaImportDto
{
    [Name(GtfsColumnNames.AreaId)]
    public string GtfsAreaId { get; set; } = default!;

    [Name(GtfsColumnNames.StopId)]
    public string GtfsStopId { get; set; } = default!;
}
