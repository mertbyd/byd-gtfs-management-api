namespace Byd.GtfsManagement.Dtos.Services;

// islevi: GTFS service_id kaydinin istemciden gelen duzenlenebilir alanini tasir.
// sistemdeki gorevi: Olusturma ve guncelleme isteklerinin ortak alan sozlesmesidir.
public abstract class ServiceInputDto
{
    public string GtfsServiceId { get; set; } = default!;
}
