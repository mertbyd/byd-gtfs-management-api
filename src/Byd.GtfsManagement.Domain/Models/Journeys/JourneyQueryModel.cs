using System;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Yolculuk planlama isteginin baslangic, varis, tarih, saat ve aktarma sinirini tasir.
// sistemdeki gorevi: Planlayicinin girdisidir.
public class JourneyQueryModel
{
    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
    public DateOnly Date { get; set; }
    public string Time { get; set; } = default!;
    public int MaxTransfers { get; set; }
}
