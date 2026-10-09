using System;

namespace Byd.GtfsManagement.Dtos.Frequencies;

// islevi: frequencies.txt satirini ve bagli seferin adini tasir.
// sistemdeki gorevi: Persistence entity'sini public cevap sozlesmesinden ayirir.
// FeedId kapsam anahtaridir; Feed adi Feed yonetiminden okunur.
public class FrequencyDto
{
    public Guid Id { get; set; }
    public Guid FeedId { get; set; }
    public Guid TripId { get; set; }
    public string TripName { get; set; } = default!;
    public string StartTime { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public int HeadwaySecs { get; set; }
    public int? ExactTimes { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
