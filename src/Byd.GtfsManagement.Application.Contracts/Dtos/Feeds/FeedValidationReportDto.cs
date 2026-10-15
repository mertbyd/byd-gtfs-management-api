using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Kanonik GTFS dogrulama sonucunu onem derecesine gore tasir.
// sistemdeki gorevi: Yukleme kaydinda saklanan raporun public cevap sozlesmesidir.
public class FeedValidationReportDto
{
    public string? ValidatorVersion { get; set; }
    public DateTimeOffset? ValidatedAt { get; set; }
    public List<FeedValidationNoticeDto> Errors { get; set; } = [];
    public List<FeedValidationNoticeDto> Warnings { get; set; } = [];
    public List<FeedValidationNoticeDto> Info { get; set; } = [];
}
