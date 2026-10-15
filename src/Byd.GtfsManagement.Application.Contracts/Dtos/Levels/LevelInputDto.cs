namespace Byd.GtfsManagement.Dtos.Levels;

// islevi: Level ekleme ve guncelleme istekleri icin ortak GTFS alanlarini tasir.
// sistemdeki gorevi: HTTP girdi sozlesmesini tanimlar.
/// <summary>GTFS kat ortak girdi DTO alanlarini tasir.</summary>
public class LevelInputDto
{
    public string GtfsLevelId { get; set; } = default!;
    public double LevelIndex { get; set; }
    public string? Name { get; set; }
}
