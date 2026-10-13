using System;

namespace Byd.GtfsManagement.Dtos.BookingRules;

// islevi: Yeni rezervasyon kurali kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS rezervasyon kurali olusturma istegini tasir.</summary>
public sealed class CreateBookingRuleDto : BookingRuleInputDto
{
    public Guid FeedId { get; set; }
}
