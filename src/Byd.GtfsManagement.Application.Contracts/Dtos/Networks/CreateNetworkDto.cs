using System;

namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: Yeni ulasim agi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS ulasim agi olusturma istegini tasir.</summary>
public sealed class CreateNetworkDto : NetworkInputDto
{
    public Guid FeedId { get; set; }
}
