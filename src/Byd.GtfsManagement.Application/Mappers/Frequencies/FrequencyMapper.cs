using Byd.GtfsManagement.Dtos.Frequencies;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.Models.Frequencies;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Frequencies;

// islevi: Frequency DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>Sefer sikligi DTO ve modellerini esler.</summary>
[Mapper]
public partial class FrequencyMapper
    : IGtfsCrudMapper<Frequency, CreateFrequencyDto, UpdateFrequencyDto, FrequencyDto,
        FrequencyCreateModel, FrequencyUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial FrequencyCreateModel Map(CreateFrequencyDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial FrequencyUpdateModel Map(UpdateFrequencyDto input);

    // Kaydi public cevap sozlesmesine donusturur.
    public partial FrequencyDto Map(Frequency entity);
}
