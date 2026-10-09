using System.Collections.Generic;
using Byd.GtfsManagement.Entities.Routes;

namespace Byd.GtfsManagement.Models.Routes;

// islevi: Bir guzergahi sefer kaliplariyla birlikte tasir.
// sistemdeki gorevi: Guzergah detay ucunun bilesik sonucudur.
public class RouteDetailModel
{
    public Route Route { get; set; } = default!;
    public List<RoutePatternModel> Patterns { get; set; } = [];
}
