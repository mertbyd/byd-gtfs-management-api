using System;

namespace Byd.GtfsManagement.Dtos.LocationGroupStops;

// islevi: Yeni konum grubu durak iliskisi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS konum grubu durak iliskisi olusturma istegini tasir.</summary>
public sealed class CreateLocationGroupStopDto : LocationGroupStopInputDto
{
    public Guid FeedId { get; set; }
}
