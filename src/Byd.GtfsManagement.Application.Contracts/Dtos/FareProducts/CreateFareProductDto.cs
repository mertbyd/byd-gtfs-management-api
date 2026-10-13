using System;

namespace Byd.GtfsManagement.Dtos.FareProducts;

// islevi: Yeni ucret urunu kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ucret urunu olusturma istegini tasir.</summary>
public sealed class CreateFareProductDto : FareProductInputDto
{
    public Guid FeedId { get; set; }
}
