using System.Collections.Generic;

namespace Byd.GtfsManagement.Dtos.Journeys;

// islevi: Bir yolculuk secenegini kalkis, varis, sure, aktarma sayisi ve adimlariyla tasir.
// sistemdeki gorevi: Yolculuk planlama cevabinin secenek listesidir.
public class JourneyItineraryDto
{
    public string DepartureTime { get; set; } = default!;
    public string ArrivalTime { get; set; } = default!;
    public int DurationSeconds { get; set; }
    public int TransferCount { get; set; }
    public List<JourneyLegDto> Legs { get; set; } = [];
}
