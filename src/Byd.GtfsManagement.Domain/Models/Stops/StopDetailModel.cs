using System.Collections.Generic;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Transfers;

namespace Byd.GtfsManagement.Models.Stops;

// islevi: Bir duragi alt peronlari, hizmet veren guzergahlari, pathway ve transfer kayitlariyla birlikte tasir.
// sistemdeki gorevi: Durak detay ucunun tek seferde okudugu bilesik sonuctur.
public class StopDetailModel
{
    public Stop Stop { get; set; } = default!;
    public List<Stop> Children { get; set; } = [];
    public List<Route> Routes { get; set; } = [];
    public List<Pathway> Pathways { get; set; } = [];
    public List<Transfer> Transfers { get; set; } = [];
}
