using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.GtfsManagement.Dtos.StopTimes;
using Byd.GtfsManagement.Dtos.Stops;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Mappers.Stops;
using Byd.GtfsManagement.Models.Stops;

namespace Byd.GtfsManagement.Services.Stops;

// islevi: Stop kayitlarinin CRUD akisini ortak tabana baglar; harita, detay ve kalkis okumalarini sunar.
// sistemdeki gorevi: Tipli baglari bildirir; CRUD tabanda kalir, okuma kurallari StopManager'dadir.
public class StopAppService
    : GtfsCrudAppServiceBase<
        Stop, CreateStopDto, UpdateStopDto, StopDto, StopFilter,
        StopCreateModel, StopUpdateModel, StopManager, IStopRepository, StopMapper>, IStopAppService
{
    // Verilen ya da aktif Feed'in duraklarini harita icin getirir.
    public virtual async Task<List<StopDto>> GetMapAsync(Guid? feedId, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetMapAsync(feedId, cancellationToken));

    // Duragi alt peronlari, ugrayan guzergahlari, pathway ve transfer kayitlariyla getirir.
    public virtual async Task<StopDetailDto> GetDetailAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.Map(await Manager.GetDetailAsync(id, cancellationToken));

    // Kalkis girdisini dogrular ve duraktan servis gunundeki kalkislari getirir.
    public virtual async Task<List<StopDepartureDto>> GetDeparturesAsync(
        Guid id, StopDepartureInputDto input, CancellationToken cancellationToken = default)
    {
        await Validator<StopDepartureInputDto>().ValidateAndThrowAsync(input, cancellationToken);
        return Mapper.Map(await Manager.GetDeparturesAsync(id, Mapper.Map(input), cancellationToken));
    }
}
