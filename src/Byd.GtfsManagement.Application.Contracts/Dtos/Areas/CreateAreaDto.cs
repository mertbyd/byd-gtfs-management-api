using System;

namespace Byd.GtfsManagement.Dtos.Areas;

// mirastan gelen: GTFS bolge kimligi ve bolge adi.
// burada yazilan: hedef Feed.
// islevi: Yeni bolge kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS bolge olusturma istegini tasir.</summary>
public sealed class CreateAreaDto : AreaInputDto
{
    public Guid FeedId { get; set; }
}
