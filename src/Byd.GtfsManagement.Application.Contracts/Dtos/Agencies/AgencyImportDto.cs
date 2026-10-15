using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Agencies;

// islevi: agency.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Agency ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Agency, GtfsImportOrders.Agency)]
public class AgencyImportDto
{
    [Name(GtfsColumnNames.AgencyId), GtfsReference(AgencyConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.CemvSupport), GtfsReference(CemvSupportConsts.TableName)]
    public int? CemvSupportId { get; set; }

    [Name(GtfsColumnNames.AgencyId)]
    public string? GtfsAgencyId { get; set; }

    [Name(GtfsColumnNames.AgencyName)]
    public string Name { get; set; } = default!;

    [Name(GtfsColumnNames.AgencyUrl)]
    public string Url { get; set; } = default!;

    [Name(GtfsColumnNames.AgencyTimezone)]
    public string Timezone { get; set; } = default!;

    [Name(GtfsColumnNames.AgencyLang)]
    public string? Language { get; set; }

    [Name(GtfsColumnNames.AgencyPhone)]
    public string? Phone { get; set; }

    [Name(GtfsColumnNames.AgencyFareUrl)]
    public string? FareUrl { get; set; }

    [Name(GtfsColumnNames.AgencyEmail)]
    public string? Email { get; set; }
}
