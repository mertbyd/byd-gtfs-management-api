using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Attributions;

// islevi: attributions.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Attribution ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Attributions, GtfsImportOrders.Attribution)]
public class AttributionImportDto
{
    [Name(GtfsColumnNames.AgencyId), GtfsReference(AgencyConsts.TableName)]
    public Guid? AgencyId { get; set; }

    [Name(GtfsColumnNames.RouteId), GtfsReference(RouteConsts.TableName)]
    public Guid? RouteId { get; set; }

    [Name(GtfsColumnNames.TripId), GtfsReference(TripConsts.TableName)]
    public Guid? TripId { get; set; }

    [Name(GtfsColumnNames.AttributionId)]
    public string? GtfsAttributionId { get; set; }

    [Name(GtfsColumnNames.OrganizationName)]
    public string OrganizationName { get; set; } = default!;

    [Name(GtfsColumnNames.IsProducer)]
    public int? IsProducer { get; set; }

    [Name(GtfsColumnNames.IsOperator)]
    public int? IsOperator { get; set; }

    [Name(GtfsColumnNames.IsAuthority)]
    public int? IsAuthority { get; set; }

    [Name(GtfsColumnNames.AttributionUrl)]
    public string? AttributionUrl { get; set; }

    [Name(GtfsColumnNames.AttributionEmail)]
    public string? AttributionEmail { get; set; }

    [Name(GtfsColumnNames.AttributionPhone)]
    public string? AttributionPhone { get; set; }
}
