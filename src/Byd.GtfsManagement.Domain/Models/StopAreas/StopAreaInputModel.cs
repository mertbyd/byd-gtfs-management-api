namespace Byd.GtfsManagement.Models.StopAreas;

// islevi: StopArea olusturma ve guncelleme icin ortak domain alanlarini tasir.
// sistemdeki gorevi: Manager islemlerine dogrulanabilir veri modeli sunar.
/// <summary>GTFS durak bolge iliskisi ortak domain girdi modelini tasir.</summary>
public abstract class StopAreaInputModel
{
    public string GtfsAreaId { get; set; } = default!;
    public string GtfsStopId { get; set; } = default!;
}
