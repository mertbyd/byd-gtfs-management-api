using System;

namespace Byd.GtfsManagement.Dtos.FareRules;

// mirastan gelen: GTFS ucret kurali baglari ve bolge alanlari.
// burada yazilan: hedef Feed.
// islevi: Yeni ucret kuralinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret kurali olusturma istegini tasir.</summary>
public sealed class CreateFareRuleDto : FareRuleInputDto
{
    public Guid FeedId { get; set; }
}
