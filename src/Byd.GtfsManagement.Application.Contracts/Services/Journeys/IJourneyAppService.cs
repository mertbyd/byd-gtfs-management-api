using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Journeys;
using Volo.Abp.Application.Services;

namespace Byd.GtfsManagement.Services.Journeys;

// islevi: Iki durak arasinda yolculuk planlama API sozlesmesini bildirir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IJourneyAppService : IApplicationService
{
    Task<JourneyPlanDto> PlanAsync(JourneyPlanInputDto input, CancellationToken cancellationToken = default);
}
