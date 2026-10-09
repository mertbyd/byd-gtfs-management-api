using System;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: Durak kalkis sorgusunun servis gununu, baslangic saatini ve kalkis sinirini tasir.
// sistemdeki gorevi: Kalkis ucunun query girdisidir.
public class StopDepartureInputDto
{
    public DateOnly Date { get; set; }
    public string? FromTime { get; set; }
    public int Limit { get; set; } = StopConsts.DefaultDepartureLimit;
}
