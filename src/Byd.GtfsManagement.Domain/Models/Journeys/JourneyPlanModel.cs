using System.Collections.Generic;
using Byd.GtfsManagement.Entities.Stops;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Planlama sonucunu baslangic ve varis duraklari ile secenek listesi olarak tasir.
// sistemdeki gorevi: Yolculuk ucunun bilesik sonucudur.
public class JourneyPlanModel
{
    public Stop FromStop { get; set; } = default!;
    public Stop ToStop { get; set; } = default!;
    public List<JourneyItineraryModel> Itineraries { get; set; } = [];
}
