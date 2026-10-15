namespace Byd.GtfsManagement.Interfaces;

// islevi: Baska GTFS satirlarinin baglandigi kaydin GTFS kimligini verir.
// sistemdeki gorevi: Disa aktarim, kayda bakan yabanci anahtarlari bu degerle GTFS dosyasindaki kimlige geri cevirir; boylece
// her tablo icin ayri bir ceviri yazilmaz.
public interface IHasGtfsId
{
    // Kaydin GTFS dosyasindaki kimligi (stop_id, route_id, trip_id ...).
    string? GtfsId { get; }
}
