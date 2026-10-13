using System.Collections.Generic;

namespace Byd.GtfsManagement.Dtos.Routes;

// islevi: Guzergahi sefer kaliplari, kaliplarin duraklari ve sekilleriyle tek cevapta tasir.
// sistemdeki gorevi: Guzergah ekraninin seferleri ve duraklari tek tek sorgulamadan okumasini saglar.
public class RouteDetailDto
{
    public RouteDto Route { get; set; } = default!;
    public List<RoutePatternDto> Patterns { get; set; } = [];
}
