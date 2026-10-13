using System.Collections.Generic;
using Byd.GtfsManagement.Dtos.Stops;

namespace Byd.GtfsManagement.Dtos.Journeys;

// islevi: Planlama sonucunu baslangic ve varis duraklari ile yolculuk secenekleri olarak tasir.
// sistemdeki gorevi: Yolculuk planlama ucunun cevabidir.
public class JourneyPlanDto
{
    public StopDto FromStop { get; set; } = default!;
    public StopDto ToStop { get; set; } = default!;
    public List<JourneyItineraryDto> Itineraries { get; set; } = [];
}
