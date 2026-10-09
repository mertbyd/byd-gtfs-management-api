using System;

namespace Byd.GtfsManagement.Dtos.Timeframes;

// islevi: Yeni zaman araligi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS zaman araligi olusturma istegini tasir.</summary>
public sealed class CreateTimeframeDto : TimeframeInputDto
{
    public Guid FeedId { get; set; }
}
