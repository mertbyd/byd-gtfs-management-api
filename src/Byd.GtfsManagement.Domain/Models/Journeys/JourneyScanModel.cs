using System;
using System.Collections.Generic;

namespace Byd.GtfsManagement.Models.Journeys;

// islevi: Bir planlama taramasinin aktarma sayisina gore varis etiketlerini, binis noktalarini ve yurume yollarini tasir.
// sistemdeki gorevi: JourneyManager'in tarama adimlari ayni durumu bu modelle paylasir; istek bitince atilir.
public class JourneyScanModel
{
    public Dictionary<Guid, JourneyLabelModel>[] Labels { get; set; } = [];
    public Dictionary<Guid, int>[] Ready { get; set; } = [];
    public Dictionary<Guid, JourneyBoardingModel> Boardings { get; set; } = [];
    public Dictionary<Guid, List<JourneyFootpathModel>> Edges { get; set; } = [];
    public Dictionary<Guid, List<JourneyWalkModel>> Reachable { get; set; } = [];
}
