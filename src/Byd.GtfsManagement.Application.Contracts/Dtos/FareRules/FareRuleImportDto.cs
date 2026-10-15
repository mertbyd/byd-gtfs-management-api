using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareRules;

// islevi: fare_rules.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareRule ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareRules, GtfsImportOrders.FareRule)]
public class FareRuleImportDto
{
    [Name(GtfsColumnNames.FareId), GtfsReference(FareAttributeConsts.TableName)]
    public Guid FareAttributeId { get; set; }

    [Name(GtfsColumnNames.RouteId), GtfsReference(RouteConsts.TableName)]
    public Guid? RouteId { get; set; }

    [Name(GtfsColumnNames.OriginId)]
    public string? OriginId { get; set; }

    [Name(GtfsColumnNames.DestinationId)]
    public string? DestinationId { get; set; }

    [Name(GtfsColumnNames.ContainsId)]
    public string? ContainsId { get; set; }
}
