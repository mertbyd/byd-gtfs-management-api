namespace Byd.GtfsManagement.Models.Agencies;

// islevi: agency.txt kaydinin duzenlenebilir alanlarini tasir.
// sistemdeki gorevi: Olusturma ve guncellemenin ayni normalizasyon yolundan gecmesini saglar.
public abstract class AgencyInputModel
{
    public int? CemvSupportId { get; set; }
    public string? GtfsAgencyId { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public string Timezone { get; set; } = default!;
    public string? Language { get; set; }
    public string? Phone { get; set; }
    public string? FareUrl { get; set; }
    public string? Email { get; set; }
}
