using Byd.GtfsManagement.Dtos.Shapes;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Models.Shapes;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Shapes;

// islevi: Shape DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS sekil DTO ve modellerini esler.</summary>
[Mapper]
public partial class ShapeMapper
    : IGtfsArchiveMapper<ShapeImportDto, Shape>, IGtfsCrudMapper<Shape, CreateShapeDto, UpdateShapeDto, ShapeDto,
        ShapeCreateModel, ShapeUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial ShapeCreateModel Map(CreateShapeDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial ShapeUpdateModel Map(UpdateShapeDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial ShapeDto Map(Shape entity);

    // CSV satirini entity'ye esler.
    public partial Shape Map(ShapeImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial ShapeImportDto MapToImport(Shape entity);
}
