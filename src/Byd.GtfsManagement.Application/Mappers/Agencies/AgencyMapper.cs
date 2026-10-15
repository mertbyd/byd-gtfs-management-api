using Byd.GtfsManagement.Dtos.Agencies;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Models.Agencies;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Agencies;

// islevi: Agency domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class AgencyMapper
    : IGtfsArchiveMapper<AgencyImportDto, Agency>, IGtfsCrudMapper<Agency, CreateAgencyDto, UpdateAgencyDto, AgencyDto, AgencyCreateModel, AgencyUpdateModel>
{
    public partial AgencyCreateModel Map(CreateAgencyDto input);
    public partial AgencyUpdateModel Map(UpdateAgencyDto input);
    public partial AgencyDto Map(Agency entity);

    // CSV satirini entity'ye esler.
    public partial Agency Map(AgencyImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial AgencyImportDto MapToImport(Agency entity);
}
