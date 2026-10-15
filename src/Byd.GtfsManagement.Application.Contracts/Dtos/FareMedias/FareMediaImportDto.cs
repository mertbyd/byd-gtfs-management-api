using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareMedias;

// islevi: fare_media.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareMedia ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareMedia, GtfsImportOrders.FareMedia)]
public class FareMediaImportDto
{
    [Name(GtfsColumnNames.FareMediaId)]
    public string GtfsFareMediaId { get; set; } = default!;

    [Name(GtfsColumnNames.FareMediaName)]
    public string? FareMediaName { get; set; }

    [Name(GtfsColumnNames.FareMediaType)]
    public int FareMediaType { get; set; }
}
