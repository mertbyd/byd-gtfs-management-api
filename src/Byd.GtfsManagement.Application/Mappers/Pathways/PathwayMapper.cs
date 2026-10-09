using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Models.Pathways;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Pathways;

// islevi: Pathway DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS yuruyus yolu DTO ve modellerini esler.</summary>
[Mapper]
public partial class PathwayMapper
    : IGtfsCrudMapper<Pathway, CreatePathwayDto, UpdatePathwayDto, PathwayDto,
        PathwayCreateModel, PathwayUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial PathwayCreateModel Map(CreatePathwayDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial PathwayUpdateModel Map(UpdatePathwayDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial PathwayDto Map(Pathway entity);
}
