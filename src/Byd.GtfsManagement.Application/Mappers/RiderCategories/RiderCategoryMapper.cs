using Byd.GtfsManagement.Dtos.RiderCategories;
using Byd.GtfsManagement.Entities.RiderCategories;
using Byd.GtfsManagement.Models.RiderCategories;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.RiderCategories;

// islevi: RiderCategory DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS yolcu kategorisi DTO ve modellerini esler.</summary>
[Mapper]
public partial class RiderCategoryMapper
    : IGtfsCrudMapper<RiderCategory, CreateRiderCategoryDto, UpdateRiderCategoryDto, RiderCategoryDto,
        RiderCategoryCreateModel, RiderCategoryUpdateModel>
{
    public partial RiderCategoryCreateModel Map(CreateRiderCategoryDto input);
    public partial RiderCategoryUpdateModel Map(UpdateRiderCategoryDto input);
    public partial RiderCategoryDto Map(RiderCategory entity);
}
