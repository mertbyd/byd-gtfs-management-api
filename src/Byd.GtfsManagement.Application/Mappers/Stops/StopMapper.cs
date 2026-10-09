using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Models.Stops;
using Byd.GtfsManagement.Mappers;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Stops;

// islevi: Stop domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class StopMapper
    : IGtfsCrudMapper<Stop, CreateStopDto, UpdateStopDto, StopDto, StopCreateModel, StopUpdateModel>
{
    public partial StopCreateModel Map(CreateStopDto input);
    public partial StopUpdateModel Map(UpdateStopDto input);
    public partial StopDto Map(Stop entity);
}
