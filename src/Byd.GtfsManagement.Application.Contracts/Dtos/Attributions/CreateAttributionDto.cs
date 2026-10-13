using System;

namespace Byd.GtfsManagement.Dtos.Attributions;

// mirastan gelen: GTFS atif kurumu, rolleri, baglari ve iletisim alanlari.
// burada yazilan: hedef Feed.
// islevi: Yeni atif kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS atif olusturma istegini tasir.</summary>
public sealed class CreateAttributionDto : AttributionInputDto
{
    public Guid FeedId { get; set; }
}
