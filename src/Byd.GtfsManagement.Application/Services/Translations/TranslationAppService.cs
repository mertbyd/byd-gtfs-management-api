using Byd.GtfsManagement.Dtos.Translations;
using Byd.GtfsManagement.Entities.Translations;
using Byd.GtfsManagement.Interfaces.Translations;
using Byd.GtfsManagement.Managers.Translations;
using Byd.GtfsManagement.Mappers.Translations;
using Byd.GtfsManagement.Models.Translations;

namespace Byd.GtfsManagement.Services.Translations;

// islevi: Translation CRUD ve GTFS arsiv akisini ortak tabana baglar.
// sistemdeki gorevi: Tipli Manager, repository ve Mapperly baglarini bildirir.
/// <summary>GTFS ceviri CRUD servisini ortak tabana baglar.</summary>
public class TranslationAppService
    : GtfsImportExportAppServiceBase<
        Translation, CreateTranslationDto, UpdateTranslationDto, TranslationDto, TranslationFilter,
        TranslationCreateModel, TranslationUpdateModel, TranslationManager, ITranslationRepository, TranslationMapper, TranslationImportDto>,
        ITranslationAppService
{
}
