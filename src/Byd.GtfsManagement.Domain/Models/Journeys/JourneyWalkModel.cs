using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir duraktan sure siniri icinde yuruyerek erisilen duragi toplam sure ve gecilen yollarla tasir.
// sistemdeki gorevi: Planlayici yurume etiketine bu yollari yazar; secenek geri kurulurken her yol ayri adim olur.
public class JourneyWalkModel
{
    public Guid TargetStopId { get; set; }
    public int Seconds { get; set; }
    public List<JourneyFootpathModel> Hops { get; set; } = [];
}
