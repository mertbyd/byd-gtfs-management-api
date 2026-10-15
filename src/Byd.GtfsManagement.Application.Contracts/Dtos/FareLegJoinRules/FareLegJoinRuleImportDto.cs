using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareLegJoinRules;

// islevi: fare_leg_join_rules.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FareLegJoinRule ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FareLegJoinRules, GtfsImportOrders.FareLegJoinRule)]
public class FareLegJoinRuleImportDto
{
    [Name(GtfsColumnNames.FromNetworkId)]
    public string? FromNetworkId { get; set; }
    [Name(GtfsColumnNames.ToNetworkId)]
    public string? ToNetworkId { get; set; }
    [Name(GtfsColumnNames.FromStopId)]
    public string? FromStopId { get; set; }
    [Name(GtfsColumnNames.ToStopId)]
    public string? ToStopId { get; set; }
}
