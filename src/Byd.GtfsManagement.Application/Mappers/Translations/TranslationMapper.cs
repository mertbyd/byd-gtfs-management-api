using Byd.GtfsManagement.Dtos.Translations;
using Byd.GtfsManagement.Entities.Translations;
using Byd.GtfsManagement.Models.Translations;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Translations;

// islevi: Translation DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS ceviri DTO ve modellerini esler.</summary>
[Mapper]
public partial class TranslationMapper
    : IGtfsCrudMapper<Translation, CreateTranslationDto, UpdateTranslationDto, TranslationDto,
        TranslationCreateModel, TranslationUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial TranslationCreateModel Map(CreateTranslationDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial TranslationUpdateModel Map(UpdateTranslationDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial TranslationDto Map(Translation entity);
}
