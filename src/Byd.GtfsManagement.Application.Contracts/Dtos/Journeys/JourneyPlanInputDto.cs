using System;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Journeys;

// islevi: Yolculuk planlama isteginin baslangic ve varis duragini, tarihini, saatini ve aktarma sinirini tasir.
// sistemdeki gorevi: Yolculuk planlama ucunun query girdisidir.
public class JourneyPlanInputDto
{
    public Guid FromStopId { get; set; }
    public Guid ToStopId { get; set; }
    public DateOnly Date { get; set; }
    public string Time { get; set; } = default!;
    public int MaxTransfers { get; set; } = JourneyConsts.DefaultMaxTransfers;
}
