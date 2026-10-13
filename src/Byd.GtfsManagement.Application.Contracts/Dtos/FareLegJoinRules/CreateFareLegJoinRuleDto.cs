using System;

namespace Byd.GtfsManagement.Dtos.FareLegJoinRules;

// islevi: Yeni ucret etap birlestirme kurali kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret etap birlestirme kurali olusturma istegini tasir.</summary>
public sealed class CreateFareLegJoinRuleDto : FareLegJoinRuleInputDto
{
    public Guid FeedId { get; set; }
}
