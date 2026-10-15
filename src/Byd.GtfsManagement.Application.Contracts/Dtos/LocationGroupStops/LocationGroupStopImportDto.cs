using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.LocationGroupStops;

// islevi: location_group_stops.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: LocationGroupStop ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.LocationGroupStops, GtfsImportOrders.LocationGroupStop)]
public class LocationGroupStopImportDto
{
    [Name(GtfsColumnNames.LocationGroupId)]
    public string GtfsLocationGroupId { get; set; } = default!;

    [Name(GtfsColumnNames.StopId)]
    public string GtfsStopId { get; set; } = default!;
}
