using System.Collections.Generic;
using Byd.GtfsManagement.Dtos.Pathways;
using Byd.GtfsManagement.Dtos.Routes;
using Byd.GtfsManagement.Dtos.Transfers;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: Duragi alt peronlari, ugrayan guzergahlari, pathway ve transfer kayitlariyla tek cevapta tasir.
// sistemdeki gorevi: Durak ekraninin iliskili kayitlari tek tek sorgulamadan okumasini saglar.
public class StopDetailDto
{
    public StopDto Stop { get; set; } = default!;
    public List<StopDto> Children { get; set; } = [];
    public List<RouteDto> Routes { get; set; } = [];
    public List<PathwayDto> Pathways { get; set; } = [];
    public List<TransferDto> Transfers { get; set; } = [];
}
