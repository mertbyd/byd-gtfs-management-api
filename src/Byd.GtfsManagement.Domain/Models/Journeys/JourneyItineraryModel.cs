using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir yolculuk secenegini kalkis, varis, aktarma sayisi ve adimlariyla tasir.
// sistemdeki gorevi: Planlayici her aktarma sayisi icin en erken varan secenegi bu modelle dondurur.
public class JourneyItineraryModel
{
    public string DepartureTime { get; set; } = default!;
    public string ArrivalTime { get; set; } = default!;
    public int DurationSeconds { get; set; }
    public int TransferCount { get; set; }
    public List<JourneyLegModel> Legs { get; set; } = [];
}
