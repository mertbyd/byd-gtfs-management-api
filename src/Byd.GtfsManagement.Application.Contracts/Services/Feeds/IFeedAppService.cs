using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Dtos.Feeds;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Byd.GtfsManagement.Services.Feeds;

// islevi: GTFS ZIP yukleme, kaynak servisten cekme, Feed okuma, gecmis listeleme ve aktiflestirme API sozlesmesini bildirir.
// sistemdeki gorevi: HTTP ve modul tuketicilerini implementation'dan ayirir.
public interface IFeedAppService : IApplicationService
{
    Task<FeedDetailDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<FeedDetailDto> GetIncludingInactiveAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResultDto<FeedDto>> GetFilteredListAsync(
        DataTableListInput<FeedFilter> input, CancellationToken cancellationToken = default);
    Task<FeedDto> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
}
