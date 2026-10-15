using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Byd.GtfsManagement.Services;

// islevi: Feed kapsamli GTFS kayitlarinin olustur, oku, listele, guncelle ve sil API sozlesmesini tek yerde bildirir.
// sistemdeki gorevi: Yaprak servis arayuzleri bu sozlesmeyi kendi DTO tipleriyle devralir; ortak CRUD tabani uygular.
public interface IGtfsCrudAppService<TCreateDto, TUpdateDto, TDto, TFilter> : IApplicationService
    where TFilter : class, IFeedFilter
{
    Task<TDto> CreateAsync(TCreateDto input, CancellationToken cancellationToken = default);
    Task<TDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TDto> GetForFeedAsync(Guid feedId, Guid id, CancellationToken cancellationToken = default);
    Task<PagedResultDto<TDto>> GetFilteredListForFeedAsync(
        Guid feedId, DataTableListInput<TFilter> input, CancellationToken cancellationToken = default);
    Task<PagedResultDto<TDto>> GetFilteredListAsync(
        DataTableListInput<TFilter> input, CancellationToken cancellationToken = default);
    Task<TDto> UpdateAsync(Guid id, TUpdateDto input, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
