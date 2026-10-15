using Byd.GtfsManagement.Dtos.Areas;
using Byd.GtfsManagement.Entities.Areas;
using Byd.GtfsManagement.Models.Areas;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Areas;

// islevi: Area DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS bolge DTO ve modellerini esler.</summary>
[Mapper]
public partial class AreaMapper
    : IGtfsArchiveMapper<AreaImportDto, Area>, IGtfsCrudMapper<Area, CreateAreaDto, UpdateAreaDto, AreaDto,
        AreaCreateModel, AreaUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial AreaCreateModel Map(CreateAreaDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial AreaUpdateModel Map(UpdateAreaDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial AreaDto Map(Area entity);

    // CSV satirini entity'ye esler.
    public partial Area Map(AreaImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial AreaImportDto MapToImport(Area entity);
}
