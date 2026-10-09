using System;
using Byd.GtfsManagement.Journeys;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Iki durak arasindaki tek yurume, pathway veya aktarma yolunu suresi ve tabela metniyle tasir.
// sistemdeki gorevi: Planlayici bir duraga varisi bu yollarla komsu duraklara yayar; her yol yolculukta ayri adim olur.
public class JourneyFootpathModel
{
    public Guid TargetStopId { get; set; }
    public int Seconds { get; set; }
    public JourneyLegKind Kind { get; set; }
    public int? PathwayMode { get; set; }
    public string? SignpostedAs { get; set; }
}
