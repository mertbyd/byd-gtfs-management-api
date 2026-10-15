using Byd.GtfsManagement.Dtos.StopAreas;
using Byd.GtfsManagement.Entities.StopAreas;
using Byd.GtfsManagement.Models.StopAreas;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.StopAreas;

// islevi: StopArea DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS durak bolge iliskisi DTO ve modellerini esler.</summary>
[Mapper]
public partial class StopAreaMapper
    : IGtfsArchiveMapper<StopAreaImportDto, StopArea>, IGtfsCrudMapper<StopArea, CreateStopAreaDto, UpdateStopAreaDto, StopAreaDto,
        StopAreaCreateModel, StopAreaUpdateModel>
{
    public partial StopAreaCreateModel Map(CreateStopAreaDto input);
    public partial StopAreaUpdateModel Map(UpdateStopAreaDto input);
    public partial StopAreaDto Map(StopArea entity);

    // CSV satirini entity'ye esler.
    public partial StopArea Map(StopAreaImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial StopAreaImportDto MapToImport(StopArea entity);
}
