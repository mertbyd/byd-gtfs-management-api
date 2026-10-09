namespace Byd.GtfsManagement.Models.Areas;

// islevi: Area olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS bolge ortak domain girdi modelini tasir.</summary>
public abstract class AreaInputModel
{
    public string GtfsAreaId { get; set; } = default!;
    public string? AreaName { get; set; }
}
