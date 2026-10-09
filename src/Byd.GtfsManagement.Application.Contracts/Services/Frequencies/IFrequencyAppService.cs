using Byd.GtfsManagement.Dtos.Frequencies;

namespace Byd.GtfsManagement.Services.Frequencies;

// islevi: Frequency CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>Sefer sikligi CRUD API sozlesmesini bildirir.</summary>
public interface IFrequencyAppService
    : IGtfsCrudAppService<CreateFrequencyDto, UpdateFrequencyDto, FrequencyDto, FrequencyFilter>
{
}
