using System;

namespace Byd.GtfsManagement.Dtos.RiderCategories;

// islevi: Yeni yolcu kategorisi kaydinin Feed'ini ve alanlarini tasir.
// sistemdeki gorevi: Manager'in kapsamli kayit kurmasini saglar.
/// <summary>GTFS yolcu kategorisi olusturma istegini tasir.</summary>
public sealed class CreateRiderCategoryDto : RiderCategoryInputDto
{
    public Guid FeedId { get; set; }
}
