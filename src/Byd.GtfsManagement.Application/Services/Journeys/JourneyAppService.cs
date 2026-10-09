using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.GtfsManagement.Dtos.Journeys;
using Byd.GtfsManagement.Managers.Journeys;
using Byd.GtfsManagement.Mappers.Journeys;

namespace Byd.GtfsManagement.Services.Journeys;

// islevi: Iki durak arasindaki yolculuk planlama use case'ini yurutur.
// sistemdeki gorevi: Girdiyi dogrular, planlamayi JourneyManager'a birakir ve sonucu public cevaba esler.
public class JourneyAppService : GtfsManagementAppService, IJourneyAppService
{
    private static readonly JourneyMapper Mapper = new();

    private JourneyManager Manager => LazyGetRequiredService<JourneyManager>();

    private IValidator<JourneyPlanInputDto> Validator => LazyGetRequiredService<IValidator<JourneyPlanInputDto>>();

    // Girdiyi dogrular ve verilen gun ve saatten sonra en erken varan yolculuk seceneklerini getirir.
    public virtual async Task<JourneyPlanDto> PlanAsync(JourneyPlanInputDto input, CancellationToken cancellationToken = default)
    {
        await Validator.ValidateAndThrowAsync(input, cancellationToken);
        return Mapper.Map(await Manager.PlanAsync(Mapper.Map(input), cancellationToken));
    }
}
