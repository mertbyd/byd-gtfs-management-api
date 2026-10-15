using Byd.GtfsManagement.Dtos.Levels;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Models.Levels;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Levels;

// islevi: Level DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS kat DTO ve modellerini esler.</summary>
[Mapper]
public partial class LevelMapper
    : IGtfsArchiveMapper<LevelImportDto, Level>, IGtfsCrudMapper<Level, CreateLevelDto, UpdateLevelDto, LevelDto,
        LevelCreateModel, LevelUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial LevelCreateModel Map(CreateLevelDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial LevelUpdateModel Map(UpdateLevelDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial LevelDto Map(Level entity);

    // CSV satirini entity'ye esler.
    public partial Level Map(LevelImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial LevelImportDto MapToImport(Level entity);
}
