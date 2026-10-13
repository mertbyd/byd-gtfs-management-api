using System;

namespace Byd.GtfsManagement.Dtos.FareTransferRules;

// islevi: Yeni ucret aktarma kurali kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret aktarma kurali olusturma istegini tasir.</summary>
public sealed class CreateFareTransferRuleDto : FareTransferRuleInputDto
{
    public Guid FeedId { get; set; }
}
