using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: fare_transfer_rules.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareTransferRule ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareTransferRules, GtfsImportOrders.FareTransferRule)]
public class FareTransferRuleImportDto
{
    [Name(GtfsColumnNames.FromLegGroupId)]
    public string? FromLegGroupId { get; set; }

    [Name(GtfsColumnNames.ToLegGroupId)]
    public string? ToLegGroupId { get; set; }
    [Name(GtfsColumnNames.FareProductId)]
    public string? GtfsFareProductId { get; set; }

    [Name(GtfsColumnNames.TransferCount)]
    public int? TransferCount { get; set; }

    [Name(GtfsColumnNames.DurationLimit)]
    public int? DurationLimit { get; set; }

    [Name(GtfsColumnNames.DurationLimitType)]
    public int? DurationLimitType { get; set; }

    [Name(GtfsColumnNames.FareTransferType)]
    public int FareTransferType { get; set; }

}
