using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareAttributes;

// islevi: fare_attributes.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareAttribute ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareAttributes, GtfsImportOrders.FareAttribute)]
public class FareAttributeImportDto
{
    [Name(GtfsColumnNames.FareId), GtfsReference(FareAttributeConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.AgencyId), GtfsReference(AgencyConsts.TableName)]
    public Guid? AgencyId { get; set; }

    [Name(GtfsColumnNames.FareId)]
    public string GtfsFareId { get; set; } = default!;

    [Name(GtfsColumnNames.Price)]
    public decimal Price { get; set; }

    [Name(GtfsColumnNames.CurrencyType)]
    public string CurrencyType { get; set; } = default!;

    [Name(GtfsColumnNames.PaymentMethod)]
    public int PaymentMethod { get; set; }

    [Name(GtfsColumnNames.Transfers)]
    public int? Transfers { get; set; }

    [Name(GtfsColumnNames.TransferDuration)]
    public int? TransferDuration { get; set; }
}
