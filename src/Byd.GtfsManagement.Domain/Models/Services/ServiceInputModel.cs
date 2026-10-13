namespace Byd.GtfsManagement.Models.Services;

// islevi: GTFS service_id kaydinin duzenlenebilir alanini tasir.
// sistemdeki gorevi: Olusturma ve guncellemenin ayni normalizasyon yolundan gecmesini saglar.
public abstract class ServiceInputModel
{
    public string GtfsServiceId { get; set; } = default!;
}
