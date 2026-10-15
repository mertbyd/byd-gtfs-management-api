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

    // Yuklenen ZIP'i yeni pasif surum olarak kaydeder; yayinlanan surumu degistirmez.
    public virtual Task<FeedDetailDto> UploadAsync(
        UploadFeedDto input, CancellationToken cancellationToken = default)
        => UploadArchiveAsync(input, null, cancellationToken);

    // Secilen Feed'in URL'sinden ZIP'i ceker; adresin o andaki degeri yeni surumde saklanir.
    public virtual async Task<FeedDetailDto> ImportFromSourceAsync(
        ImportFeedDto input, CancellationToken cancellationToken = default)
    {
        await ImportValidator.ValidateAndThrowAsync(input, cancellationToken);
        var sourceUrl = input.Url!;
        var archive = await FeedDownloader.DownloadAsync(sourceUrl, cancellationToken);
        var fileName = Path.GetFileName(Uri.UnescapeDataString(new Uri(sourceUrl).Segments[^1]).Replace('\\', '/'));
        if (fileName.Length > FeedConsts.MaxStoredFileNameLength ||
            !fileName.EndsWith(GtfsValidationConsts.ArchiveExtension, StringComparison.OrdinalIgnoreCase))
            fileName = GtfsFileNames.Archive;
        return await UploadArchiveAsync(Mapper.Map(input, fileName, archive), sourceUrl, cancellationToken);
    }

    // Aktif Feed'in guncel tablolarini yeni adli pasif Feed olarak dogrulama ve entegrasyon akisina verir.
    public virtual async Task<FeedDetailDto> CreateDraftAsync(
        Guid id, CreateDraftFeedDto input, CancellationToken cancellationToken = default)
    {
        await DraftValidator.ValidateAndThrowAsync(input, cancellationToken);
        await Manager.EnsureDraftSourceAsync(id, cancellationToken);
        var archive = await GtfsValidator.ExportArchiveAsync(id, cancellationToken);
        return await UploadAsync(Mapper.Map(input, archive.FileName, archive.Data), cancellationToken);
    }

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

    // Kayitli Feed'in verisini yeniden dogrulayip raporunu gunceller.
    public virtual Task<FeedValidationReportDto> ValidateAsync(Guid id, CancellationToken cancellationToken = default)
        => GtfsValidator.ValidateAsync(() => Task.FromResult(id), feedId => feedId, cancellationToken);

    // Pasifler dahil Feed'in temel tablo sayilarini ve servis tarih araligini getirir.
    public virtual async Task<FeedSummaryDto> GetSummaryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var feed = await Manager.EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken);
        return Mapper.Map(await Repository.GetSummaryAsync(feed, cancellationToken));
    }

    // Kayitli GTFS dogrulama raporunu JSON dosyasi olarak verir.
    public virtual Task<FeedFileDto> ExportValidationReportAsync(
        Guid id, CancellationToken cancellationToken = default)
        => GtfsValidator.ExportReportAsync(id, cancellationToken);

    // Entegre Feed'in guncel GTFS ZIP'ini indirir.
    public virtual Task<FeedFileDto> ExportGtfsAsync(Guid id, CancellationToken cancellationToken = default)
        => GtfsValidator.ExportArchiveAsync(id, cancellationToken);

    // Ortak arsiv akisi once dosyayi kalici saklar; dogrulayici erisilemese bile yukleme gecmisi kaybolmaz.
    private async Task<FeedDetailDto> UploadArchiveAsync(
        UploadFeedDto input, string? sourceUrl, CancellationToken cancellationToken = default)
    {
        await UploadValidator.ValidateAndThrowAsync(input, cancellationToken);
        var id = await StoreArchiveAsync(input, sourceUrl, cancellationToken);
        await GtfsValidator.ValidateAsync(() => Task.FromResult(id), feedId => feedId, cancellationToken);
        return Mapper.MapToDetail(await Manager.EnsureExistsIncludingInactiveInScopeAsync(id, cancellationToken));
    }

    // Surumu ve ZIP dosyasini ayni arsiv isleminde saklar; ust Feed ayni EF kapsaminda yuklenir.
    private async Task<Guid> StoreArchiveAsync(
        UploadFeedDto input, string? sourceUrl, CancellationToken cancellationToken = default)
    {
        using var archiveUnitOfWork = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: true);
        var model = Mapper.Map(input, sourceUrl);
        model.SourceType = sourceUrl is null ? FeedSourceType.Upload : FeedSourceType.Url;
        var version = await Manager.CreateAsync(model, cancellationToken);
        var category = await Manager.GetArchiveCategoryAsync(cancellationToken);
        var file = await StoredFiles.UploadAsync(Mapper.Map(input, version.Id, category.Id), cancellationToken);
        Manager.AttachStoredFile(version, file.Id, file.OriginalFileName);
        await Repository.InsertAsync(version, cancellationToken: cancellationToken);
        await archiveUnitOfWork.CompleteAsync(cancellationToken);
        return version.Id;
    }
}
