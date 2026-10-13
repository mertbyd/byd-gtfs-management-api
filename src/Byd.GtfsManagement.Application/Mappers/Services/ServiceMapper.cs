using Byd.GtfsManagement.Dtos.Services;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Models.Services;
using Byd.GtfsManagement.Mappers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Services;

// islevi: Service domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class ServiceMapper
    : IGtfsCrudMapper<Service, CreateServiceDto, UpdateServiceDto, ServiceDto, ServiceCreateModel, ServiceUpdateModel>
{
    public partial ServiceCreateModel Map(CreateServiceDto input);
    public partial ServiceUpdateModel Map(UpdateServiceDto input);
    public partial ServiceDto Map(Service entity);
}
