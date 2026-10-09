namespace Byd.GtfsManagement.Dtos.FeedInfos;

// islevi: FeedInfo ekleme ve guncelleme istekleri icin ortak GTFS alanlarini tasir.
// sistemdeki gorevi: HTTP girdi sozlesmesini tanimlar.
/// <summary>GTFS yayimci bilgisi ortak girdi DTO alanlarini tasir.</summary>
public class FeedInfoInputDto
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
