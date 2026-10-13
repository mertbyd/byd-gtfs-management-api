using System;

namespace Byd.GtfsManagement.Interfaces;

// islevi: Entity'nin bir GTFS Feed'e bagli oldugunu bildirir.
// sistemdeki gorevi: Feed kapsamli kayitlarin ortak kapsam kontrolunden gecebilmesini saglar; kapsam dogrulamasi
// bu alani okur, boylece her varlik icin ayri bir kontrol yazilmaz.
public interface IHasFeed
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    Guid FeedId { get; }
}
