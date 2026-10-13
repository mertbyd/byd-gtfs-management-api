using System;

namespace Byd.GtfsManagement.Dtos.FeedInfos;

// islevi: feed_info.txt satirini ve yayimci alanlarini public cevapta tasir.
// sistemdeki gorevi: Persistence entity'sini HTTP sozlesmesinden ayirir.
// FeedId, GTFS kaydinin kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
/// <summary>GTFS yayimci bilgisi cevap alanlarini tasir.</summary>
public class FeedInfoDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public string PublisherName { get; set; } = default!;
    public string PublisherUrl { get; set; } = default!;
    public string Language { get; set; } = default!;
    public string? DefaultLanguage { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public string? Version { get; set; }
    public string? ContactEmail { get; set; }
    public string? ContactUrl { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
