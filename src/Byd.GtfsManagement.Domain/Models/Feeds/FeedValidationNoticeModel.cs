using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Tek GTFS kural ihlalinin kodunu, siddetini ve orneklerini tasir.
// sistemdeki gorevi: Saklanan raporun notice grubudur.
public class FeedValidationNoticeModel
{
    public string Code { get; set; } = default!;
    public string Severity { get; set; } = default!;
    public int TotalNotices { get; set; }
    public List<FeedValidationOccurrenceModel> Samples { get; set; } = [];
}
