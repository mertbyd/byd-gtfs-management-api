namespace Byd.GtfsManagement.Models.FeedInfos;

// islevi: FeedInfo ekleme ve guncelleme icin ortak alanlari tasir.
// sistemdeki gorevi: Yayimci bilgilerini Domain katmanina tasir.
/// <summary>GTFS yayimci bilgisi ortak girdi modelini tasir.</summary>
public class FeedInfoInputModel
{
    public string PublisherName { get; set; } = default!;
    public string PublisherUrl { get; set; } = default!;
    public string Language { get; set; } = default!;
    public string? DefaultLanguage { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Version { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactUrl { get; set; }
}
