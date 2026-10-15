using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: location_groups.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: LocationGroup ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.LocationGroups, GtfsImportOrders.LocationGroup)]
public class LocationGroupImportDto
{
    [Name(GtfsColumnNames.LocationGroupId), GtfsReference(LocationGroupConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.LocationGroupId)]
    public string GtfsLocationGroupId { get; set; } = default!;

    [Name(GtfsColumnNames.LocationGroupName)]
    public string? LocationGroupName { get; set; }
}
