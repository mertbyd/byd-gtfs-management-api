using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Kanonik GTFS dogrulama sonucunu onem derecesine gore tasir.
// sistemdeki gorevi: Yukleme kaydinda saklanan raporun Domain bicimidir.
public class FeedValidationReportModel
{
    public string? ValidatorVersion { get; set; }
    public DateTimeOffset? ValidatedAt { get; set; }
    public List<FeedValidationNoticeModel> Errors { get; set; } = [];
    public List<FeedValidationNoticeModel> Warnings { get; set; } = [];
    public List<FeedValidationNoticeModel> Info { get; set; } = [];
}
