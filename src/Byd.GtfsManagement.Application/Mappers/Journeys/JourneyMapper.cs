using Byd.GtfsManagement.Dtos.Journeys;
using Byd.GtfsManagement.Models.Journeys;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Journeys;

// islevi: Yolculuk planlama girdisi ve sonucu ile domain modelleri arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class JourneyMapper
{
    public partial JourneyQueryModel Map(JourneyPlanInputDto input);
    public partial JourneyPlanDto Map(JourneyPlanModel model);
}
