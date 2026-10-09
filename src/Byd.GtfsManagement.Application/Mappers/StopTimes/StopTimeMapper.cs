using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Models.StopTimes;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.StopTimes;

// islevi: StopTime domain modelleri ve public DTO'lari arasindaki eslemeleri uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>StopTime DTO ve modellerini esler.</summary>
[Mapper]
public partial class StopTimeMapper
    : IGtfsCrudMapper<StopTime, CreateStopTimeDto, UpdateStopTimeDto, StopTimeDto, StopTimeCreateModel, StopTimeUpdateModel>
{
    // Olusturma istegini domain modeline donusturur.
    public partial StopTimeCreateModel Map(CreateStopTimeDto input);

    // Guncelleme istegini domain modeline donusturur.
    public partial StopTimeUpdateModel Map(UpdateStopTimeDto input);

    // Durak zamani kaydini public cevaba donusturur.
    public partial StopTimeDto Map(StopTime entity);
}
