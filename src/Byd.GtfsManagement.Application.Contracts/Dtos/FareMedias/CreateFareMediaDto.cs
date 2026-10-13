using System;

namespace Byd.GtfsManagement.Dtos.FareMedias;

// islevi: Yeni ucret medyasi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret medyasi olusturma istegini tasir.</summary>
public sealed class CreateFareMediaDto : FareMediaInputDto
{
    public Guid FeedId { get; set; }
}
