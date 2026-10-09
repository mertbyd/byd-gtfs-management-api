using System;

namespace Byd.GtfsManagement.Dtos.FareAttributes;

// mirastan gelen: GTFS ucret alanlari, fiyati ve aktarma kurallari.
// burada yazilan: hedef Feed.
// islevi: Yeni ucret ozelliginin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret ozelligi olusturma istegini tasir.</summary>
public sealed class CreateFareAttributeDto : FareAttributeInputDto
{
    public Guid FeedId { get; set; }
}
