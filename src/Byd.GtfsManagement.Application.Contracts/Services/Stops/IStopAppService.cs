using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Dtos.Stops;

namespace Byd.GtfsManagement.Services.Stops;

// islevi: stops.txt kayitlarinin CRUD ve liste API sozlesmesini ortak GTFS CRUD sozlesmesinden devralir; harita, detay ve
// kalkis okumalarini ekler.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IStopAppService
    : IGtfsCrudAppService<CreateStopDto, UpdateStopDto, StopDto, StopFilter>
{
    Task<List<StopDto>> GetMapAsync(Guid? feedId, CancellationToken cancellationToken = default);
    Task<StopDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<StopDepartureDto>> GetDeparturesAsync(
        Guid id, StopDepartureInputDto input, CancellationToken cancellationToken = default);
    Task<StopDetailDto> GetDetailForFeedAsync(
        Guid feedId, Guid id, CancellationToken cancellationToken = default);
    Task<List<StopDto>> GetMapForFeedAsync(Guid feedId, CancellationToken cancellationToken = default);
    Task<List<StopDepartureDto>> GetDeparturesForFeedAsync(
        Guid feedId, Guid id, StopDepartureInputDto input, CancellationToken cancellationToken = default);
}
