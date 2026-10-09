using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.Interfaces.Frequencies;
using Byd.GtfsManagement.Managers.Frequencies;
using Byd.GtfsManagement.Mappers.Frequencies;
using Byd.GtfsManagement.Models.Frequencies;

namespace Byd.GtfsManagement.Services.Frequencies;

// islevi: Frequency CRUD akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>Sefer sikligi CRUD servisini ortak tabana baglar.</summary>
public class FrequencyAppService
    : GtfsCrudAppServiceBase<
        Frequency, CreateFrequencyDto, UpdateFrequencyDto, FrequencyDto, FrequencyFilter,
        FrequencyCreateModel, FrequencyUpdateModel, FrequencyManager, IFrequencyRepository, FrequencyMapper>,
        IFrequencyAppService
{
}
