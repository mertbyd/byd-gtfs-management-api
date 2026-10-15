using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Querying;
using Byd.FileManagement.Services.StoredFiles;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Mappers.Feeds;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Local;
using Volo.Abp.Uow;

namespace Byd.GtfsManagement.Services.Feeds;

// islevi: Feed'e ZIP veya URL ile surum ekleme, surum okuma, taslak, rapor ve yayin secimi akislarini orkestre
// eder.
// sistemdeki gorevi: Validator, Mapperly, Manager, repository, file modulu ve dogrulayiciyi birlestirir. Dogrulamayi gecen Feed
// Validated durumunda kaydedilir; handler hemen, periyodik isci yedek olarak tablolara yazmayi dener.
public class FeedAppService : GtfsManagementAppService, IFeedAppService
{
    private static readonly FeedMapper Mapper = new();
    private FeedManager Manager
        => LazyGetRequiredService<FeedManager>();
    private IFeedRepository Repository
        => LazyGetRequiredService<IFeedRepository>();
    private IValidator<UploadFeedDto> UploadValidator
        => LazyGetRequiredService<IValidator<UploadFeedDto>>();
    private IValidator<ImportFeedDto> ImportValidator
        => LazyGetRequiredService<IValidator<ImportFeedDto>>();
    private IValidator<CreateDraftFeedDto> DraftValidator
        => LazyGetRequiredService<IValidator<CreateDraftFeedDto>>();
    private IValidator<DataTableListInput<FeedFilter>> ListValidator
        => LazyGetRequiredService<IValidator<DataTableListInput<FeedFilter>>>();
    private IDataFilter ActiveFilter => LazyGetRequiredService<IDataFilter>();
    private IStoredFileAppService StoredFiles => LazyGetRequiredService<IStoredFileAppService>();
    private IGtfsFeedValidator GtfsValidator => LazyGetRequiredService<IGtfsFeedValidator>();
    private IGtfsFeedDownloader FeedDownloader => LazyGetRequiredService<IGtfsFeedDownloader>();
    private ILocalEventBus LocalEventBus => LazyGetRequiredService<ILocalEventBus>();
    private ICurrentOrganization CurrentOrganization => LazyGetRequiredService<ICurrentOrganization>();

    // Aktif Feed'i kapsam kuralindan gecirerek okur.
    public virtual async Task<FeedDetailDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => Mapper.MapToDetail(await Manager.EnsureExistsInScopeAsync(id, cancellationToken));

    // Yonetim icin pasif Feed'i de kapsam kuralindan gecirerek okur.
    public virtual async Task<FeedDetailDto> GetIncludingInactiveAsync(
        Guid id, CancellationToken cancellationToken = default)
        => Mapper.MapToDetail(await Manager.EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken));

    // Pasif ve basarisiz olanlar dahil tum Feed'leri yukleme gecmisi olarak filtreleyip sayfalar.
    public virtual async Task<PagedResultDto<FeedDto>> GetFilteredListAsync(
        DataTableListInput<FeedFilter> input, CancellationToken cancellationToken = default)
    {
        await ListValidator.ValidateAndThrowAsync(input, cancellationToken);
        Manager.EnsureScopeAvailable();
        using var disabled = ActiveFilter.Disable<ActiveFeedFilter>();
        var feeds = await Repository.GetFilteredListAsync(input, cancellationToken);
        var totalCount = await Repository.GetFilteredCountAsync(input, cancellationToken);
        return new PagedResultDto<FeedDto>(totalCount, Mapper.Map(feeds));
    }

    // Yerel handler ayni transaction'da yalniz ust Feed'in secili surumunu degistirir; GTFS satirlarina toplu yazim yapmaz.
    public virtual async Task<FeedDto> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var feed = await Manager.EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken);
        await LocalEventBus.PublishAsync(new FeedActivationRequestedEto
        {
            TenantId = feed.TenantId!.Value,
            OrganizationId = feed.OrganizationId!.Value,
            FeedId = feed.Id
        }, onUnitOfWorkComplete: false);
        return Mapper.Map(feed);
    }

}
