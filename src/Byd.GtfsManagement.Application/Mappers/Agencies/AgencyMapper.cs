using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Models.Agencies;
using Byd.GtfsManagement.Mappers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Agencies;

// islevi: Agency domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class AgencyMapper
    : IGtfsCrudMapper<Agency, CreateAgencyDto, UpdateAgencyDto, AgencyDto, AgencyCreateModel, AgencyUpdateModel>
{
    public partial AgencyCreateModel Map(CreateAgencyDto input);
    public partial AgencyUpdateModel Map(UpdateAgencyDto input);
    public partial AgencyDto Map(Agency entity);
}
