using System;

namespace Byd.GtfsManagement.Models.Stops;

// islevi: Bir duraktan kalkislarin servis gunu, baslangic saati ve sayi sinirini tasir.
// sistemdeki gorevi: Kalkis listesi istegini Manager'a tasir.
public class StopDepartureQueryModel
{
    public DateOnly Date { get; set; }
    public string? FromTime { get; set; }
    public int Limit { get; set; }
}
