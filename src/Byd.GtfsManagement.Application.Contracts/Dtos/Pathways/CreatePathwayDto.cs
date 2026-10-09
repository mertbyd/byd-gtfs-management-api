using System;

namespace Byd.GtfsManagement.Dtos.Pathways;

// mirastan gelen: GTFS yuruyus yolu baglari, modu ve olculeri.
// burada yazilan: hedef Feed.
// islevi: Yeni yuruyus yolunun Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS yuruyus yolu olusturma istegini tasir.</summary>
public sealed class CreatePathwayDto : PathwayInputDto
{
    public Guid FeedId { get; set; }
}
