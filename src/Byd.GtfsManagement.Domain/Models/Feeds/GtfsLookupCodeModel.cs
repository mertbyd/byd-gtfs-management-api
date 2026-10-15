namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Bir lookup satirinin tablo adini, GTFS kodunu ve kimligini tasir.
// sistemdeki gorevi: Feed ice aktariminin lookup kodlarini kimliklere cevirdigi tablonun satiridir.
public class GtfsLookupCodeModel
{
    public string TableName { get; set; } = default!;
    public int? Code { get; set; }
    public int Id { get; set; }
}
