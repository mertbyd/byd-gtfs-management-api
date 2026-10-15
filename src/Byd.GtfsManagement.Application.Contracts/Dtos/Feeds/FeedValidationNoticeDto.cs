using System.Collections.Generic;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Tek GTFS kural ihlalinin kodunu, siddetini ve orneklerini tasir.
// sistemdeki gorevi: Kanonik validator notice grubunu public cevaba cevirir.
public class FeedValidationNoticeDto
{
    public string Code { get; set; } = default!;
    public string Severity { get; set; } = default!;
    public int TotalNotices { get; set; }
    public List<FeedValidationOccurrenceDto> Samples { get; set; } = [];
}
