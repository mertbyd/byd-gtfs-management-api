using System;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// islevi: Filtre senaryosunda bir Feed'in beklenen satir kimliklerini tasir.
// sistemdeki gorevi: Beklenen sonuclari uretim sorgusundan bagimsiz olarak tanimlar.
internal sealed record FeedTestFeed(Guid FeedId, Guid StopId, Guid LevelId, Guid AgencyId);
