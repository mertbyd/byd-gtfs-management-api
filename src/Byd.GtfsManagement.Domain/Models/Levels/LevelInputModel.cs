namespace Byd.GtfsManagement.Models.Levels;

// islevi: Level ekleme ve guncelleme icin ortak alanlari tasir.
// sistemdeki gorevi: Kat girdilerini Domain katmanina tasir.
/// <summary>GTFS kat ortak girdi modelini tasir.</summary>
public class LevelInputModel
{
    public string GtfsLevelId { get; set; } = default!;
    public double LevelIndex { get; set; }
    public string? Name { get; set; }
}
