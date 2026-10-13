using System;

namespace Byd.GtfsManagement.Dtos.LocationGroups;

// islevi: Yeni konum grubu kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS konum grubu olusturma istegini tasir.</summary>
public sealed class CreateLocationGroupDto : LocationGroupInputDto
{
    public Guid FeedId { get; set; }
}
