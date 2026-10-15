namespace Byd.GtfsManagement.Options;

/// <summary>
/// Modulu tuketen host'un Configure ile verdigi calisma zamani secenekleri.
/// </summary>
// islevi: Yerel GTFS dogrulayicisinin kok adresini tasir.
// sistemdeki gorevi: Modul yapilandirma okumaz; host adresi kendi yapilandirmasindan bu secenege baglar. Dogrulayici yalniz loopback
// adresini kabul eder.
public class GtfsManagementModuleOptions
{
    // Yerel MobilityData dogrulayicisinin kok adresi (or: http://127.0.0.1:8080/v2/).
    public string? ValidatorBaseUrl { get; set; }
}
