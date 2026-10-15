using Byd.GtfsManagement.Dtos.Timeframes;
using Byd.GtfsManagement.Entities.Timeframes;
using Byd.GtfsManagement.Models.Timeframes;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Timeframes;

// islevi: Timeframe DTO, domain model ve entity eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
/// <summary>GTFS zaman araligi DTO ve modellerini esler.</summary>
[Mapper]
public partial class TimeframeMapper
    : IGtfsArchiveMapper<TimeframeImportDto, Timeframe>, IGtfsCrudMapper<Timeframe, CreateTimeframeDto, UpdateTimeframeDto, TimeframeDto,
        TimeframeCreateModel, TimeframeUpdateModel>
{
    public partial TimeframeCreateModel Map(CreateTimeframeDto input);
    public partial TimeframeUpdateModel Map(UpdateTimeframeDto input);
    public partial TimeframeDto Map(Timeframe entity);

    // CSV satirini entity'ye esler.
    public partial Timeframe Map(TimeframeImportDto input);

    // Entity'yi disa aktarim icin CSV satirina esler.
    public partial TimeframeImportDto MapToImport(Timeframe entity);
}
