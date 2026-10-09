using System;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir sefere hangi duraktan, kacinci seferle ve hangi saatte binildigini tasir.
// sistemdeki gorevi: Ayni seferin sonraki baglantilari varis etiketini bu binis noktasina baglar.
public class JourneyBoardingModel
{
    public int Round { get; set; }
    public Guid BoardStopId { get; set; }
    public int DepartureSeconds { get; set; }
}
