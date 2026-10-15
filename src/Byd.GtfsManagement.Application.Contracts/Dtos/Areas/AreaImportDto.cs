using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Areas;

// islevi: areas.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Area ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Areas, GtfsImportOrders.Area)]
public class AreaImportDto
{
    [Name(GtfsColumnNames.AreaId)]
    public string GtfsAreaId { get; set; } = default!;

    [Name(GtfsColumnNames.AreaName)]
    public string? AreaName { get; set; }
}
