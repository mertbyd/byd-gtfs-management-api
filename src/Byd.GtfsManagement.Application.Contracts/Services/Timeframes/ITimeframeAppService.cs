using Byd.GtfsManagement.Dtos.Timeframes;

namespace Byd.GtfsManagement.Services.Timeframes;

// islevi: Timeframe CRUD ve filtreli liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini uygulama sinifindan ayirir.
/// <summary>GTFS zaman araligi CRUD API sozlesmesini bildirir.</summary>
public interface ITimeframeAppService
    : IGtfsCrudAppService<CreateTimeframeDto, UpdateTimeframeDto, TimeframeDto, TimeframeFilter>
{
}
