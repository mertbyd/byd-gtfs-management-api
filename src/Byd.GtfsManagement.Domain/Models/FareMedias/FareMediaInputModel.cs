namespace Byd.GtfsManagement.Models.FareMedias;

// islevi: FareMedia olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS ucret medyasi ortak domain girdi modelini tasir.</summary>
public abstract class FareMediaInputModel
{
    public string GtfsFareMediaId { get; set; } = default!;
    public string? FareMediaName { get; set; }
    public int FareMediaType { get; set; }
}
