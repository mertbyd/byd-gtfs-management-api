using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Journeys;
using Byd.GtfsManagement.Permissions;
using Byd.GtfsManagement.Services.Journeys;
using SystemStandards.Results;

namespace Byd.GtfsManagement.Controllers.Journeys;

// islevi: Yolculuk planlama ucunun HTTP rota ve yetki metadatasini sunar.
// sistemdeki gorevi: Planlama use case'ini ince Controller uzerinden AppService'e yonlendirir.
[ApiController]
[Route(JourneyRoutes.Root)]
public class JourneyController : GtfsManagementController
{
    private IJourneyAppService AppService => LazyGetRequiredService<IJourneyAppService>();

    /// <summary>Iki durak arasinda verilen gun ve saatten sonra en erken varan yolculuk seceneklerini getirir.</summary>
    /// <param name="input">Baslangic ve varis duragi, tarih, saat ve aktarma siniri.</param>
    /// <param name="cancellationToken">Istek iptal belirteci.</param>
    /// <remarks>
    /// Request {
    ///   FromStopId   (Guid)     → Baslangic duragi veya istasyonu
    ///   ToStopId     (Guid)     → Varis duragi veya istasyonu
    ///   Date         (DateOnly) → Yolculuk gunu (yyyy-MM-dd)
    ///   Time         (string)   → Bu saatten sonra yola cikilir (HH:MM:SS)
    ///   MaxTransfers (int)      → En fazla aktarma; varsayilan 2, en fazla 4
    /// }
    /// Response value baslangic ve varis duraklarini StopDto olarak ve her aktarma sayisi icin en erken varan secenegi
    /// tasir; daha az aktarmayla daha erken varilabilen secenek listelenmez. Her secenek kalkis, varis, toplam sure ve
    /// aktarma sayisiyla adimlarini verir: Transit adimi binilen seferi TripDto, guzergahi RouteDto olarak; Walk adimi yakin duraklar arasi
    /// yurumeyi; Pathway adimi istasyon ici yolu pathway_mode ve tabela metniyle; Transfer adimi transfers.txt veya istasyon
    /// ici aktarmayi tasir. Duraklar ayni Feed'de olmalidir; secenek bulunamazsa liste bos doner. Gercek zamanli gecikme
    /// ve GTFS-Flex talebe bagli servisler bu planlamaya girmez.
    /// </remarks>
    [HttpGet(JourneyRoutes.Plan)]
    [Authorize(GtfsManagementPermissions.Journeys.View)]
    public virtual async Task<Result<JourneyPlanDto>> PlanAsync(
        [FromQuery] JourneyPlanInputDto input, CancellationToken cancellationToken = default)
        => await AppService.PlanAsync(input, cancellationToken);
}
