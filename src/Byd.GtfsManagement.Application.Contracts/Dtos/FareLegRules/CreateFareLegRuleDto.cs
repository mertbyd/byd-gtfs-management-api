using System;

namespace Byd.GtfsManagement.Dtos.FareLegRules;

// islevi: Yeni ucret etap kurali kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret etap kurali olusturma istegini tasir.</summary>
public sealed class CreateFareLegRuleDto : FareLegRuleInputDto
{
    public Guid FeedId { get; set; }
}
