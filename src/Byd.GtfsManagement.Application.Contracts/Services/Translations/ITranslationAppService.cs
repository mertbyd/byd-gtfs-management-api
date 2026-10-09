using Byd.GtfsManagement.Dtos.Translations;

namespace Byd.GtfsManagement.Services.Translations;

// islevi: Translation CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS ceviri CRUD API sozlesmesini bildirir.</summary>
public interface ITranslationAppService
    : IGtfsCrudAppService<CreateTranslationDto, UpdateTranslationDto, TranslationDto, TranslationFilter>
{
}
