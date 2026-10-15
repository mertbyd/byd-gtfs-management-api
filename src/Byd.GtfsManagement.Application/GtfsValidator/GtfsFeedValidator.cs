using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Byd.FileManagement.Services.StoredFiles;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Mappers.Feeds;
using Byd.GtfsManagement.Options;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.GtfsMapper;
using Byd.GtfsManagement.Services;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Data;
using Volo.Abp.Timing;

namespace Byd.GtfsManagement.GtfsValidator;

// islevi: Feed'i degistiren islemi calistirir; Feed'in guncel GTFS arsivini yerel kanonik dogrulayiciya gonderir, cevabi rapora
// cevirir, raporu Feed'e yazar ve dondurur. Kayitli raporu JSON dosyasi olarak verir.
// sistemdeki gorevi: Dogrulamanin tek giris noktasidir. Feed'in guncel verisi veritabanina aktarilmadan once yuklenen ZIP, aktarildiktan
// sonra tablolarin kendisidir; tablolar ice aktarimla ayni DTO ve mapper'larla geri GTFS arsivine yazilir. Arsiv yerel dogrulayicidan
// disari cikmaz; cagri log modulunun dis servis loguna yazilir.
// NEDEN MAPPERLY DEGIL (rapor okuma): sampleNotices alanlari kurala gore degisir; burada domain eslemesi degil, saglayici JSON
// protokolunun ayristirilmasi yapilir.
public class GtfsFeedValidator(
    IHttpClientFactory clientFactory,
    IOptions<GtfsManagementModuleOptions> options,
    IExternalServiceLogger externalServiceLogger,
    IStoredFileAppService storedFiles,
    IEnumerable<IGtfsImportExportAppService<GtfsArchiveContext>> tables,
    IFeedRepository repository,
    FeedManager feedManager,
    IDataFilter dataFilter,
    IDistributedEventBus distributedEventBus,
    IClock clock) : IGtfsFeedValidator
{
    private static readonly FeedMapper Mapper = new();
    private static readonly JsonSerializerOptions ReportFileOptions = new(JsonSerializerDefaults.Web) { WriteIndented = true };
    private static readonly ExternalServiceCall ValidationCall = new(
        GtfsValidationConsts.ProviderName,
        GtfsValidationConsts.MethodName,
        GtfsValidationConsts.RequestType);

    // Degisikligi calistirir, Feed'in guncel verisini dogrular, raporu Feed'e yazar ve dondurur. Rapor cagiranin isleminde yazilir;
    // dogrulayiciya ulasilamazsa hata cagirana doner.
    public virtual async Task<FeedValidationReportDto> ValidateAsync<TResult>(
        Func<Task<TResult>> change, Func<TResult, Guid> feedIdOf, CancellationToken cancellationToken = default)
    {
        var feed = await feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedIdOf(await change()), cancellationToken);
        feedManager.SetOperationUser(feed);
        var report = await ValidateArchiveAsync(feed, cancellationToken);
        feedManager.SetValidationReport(feed, Mapper.Map(report));
        await repository.UpdateAsync(feed, autoSave: true, cancellationToken: cancellationToken);
        if (feed.Status == FeedStatus.Validated)
            await distributedEventBus.PublishAsync(new FeedIntegrationRequestedEto
            {
                TenantId = feed.TenantId!.Value,
                OrganizationId = feed.OrganizationId!.Value,
                FeedId = feed.Id
            });
        return report;
    }

    // Kayitli raporu zaman damgali JSON dosyasi olarak verir; henuz raporu olmayan Feed'i lokalize hataya cevirir.
    public virtual async Task<FeedFileDto> ExportReportAsync(Guid feedId, CancellationToken cancellationToken = default)
    {
        var feed = await feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);
        var report = feed.ValidationReport ?? throw new BusinessException(FeedExceptionCodes.ValidationReportMissing);
        return new FeedFileDto
        {
            FileName = GtfsValidationConsts.ReportFileName +
                clock.Now.ToString(GtfsValidationConsts.ReportTimestampFormat, CultureInfo.InvariantCulture) +
                GtfsValidationConsts.ReportFileExtension,
            ContentType = GtfsValidationConsts.JsonMediaType,
            Data = JsonSerializer.SerializeToUtf8Bytes(Mapper.Map(report), ReportFileOptions)
        };
    }

    // Entegre Feed'in guncel tablolarini standart GTFS ZIP dosyasi olarak verir.
    public virtual async Task<FeedFileDto> ExportArchiveAsync(Guid feedId, CancellationToken cancellationToken = default)
    {
        var feed = await feedManager.EnsureExistsIncludingInactiveInScopeAsync(feedId, cancellationToken);
        if (feed.Status != FeedStatus.Integrated)
            throw new BusinessException(FeedExceptionCodes.NotIntegrated);
        var archive = await ExportAsync(feed, cancellationToken);
        return new FeedFileDto
        {
            FileName = GtfsFileNames.Archive,
            ContentType = GtfsValidationConsts.ZipMediaType,
            Data = archive
        };
    }

    // Dogrulama HTTP isteginin sonucu ayni cagrida doner; arka plan entegrasyonu dagitik olaya birakilir.
    private async Task<FeedValidationReportDto> ValidateArchiveAsync(
        Feed feed, CancellationToken cancellationToken = default)
        => await SendAsync(await GetArchiveAsync(feed, cancellationToken), feed.StoredFileName, cancellationToken);

    // Feed'in guncel GTFS arsivini verir: aktarimdan once yuklenen ZIP, aktarimdan sonra tablolardan yazilan arsiv.
    private async Task<byte[]> GetArchiveAsync(Feed feed, CancellationToken cancellationToken = default)
        => feed.Status != FeedStatus.Integrated
            ? (await storedFiles.DownloadAsync(feed.StoredFileId, cancellationToken)).Data
            : await ExportAsync(feed, cancellationToken);

    // Feed'in tablolarini ice aktarimla ayni beyan ve mapper'larla, ayni sirada GTFS ZIP'ine yazar.
    private async Task<byte[]> ExportAsync(Feed feed, CancellationToken cancellationToken = default)
    {
        using var versionsIncluded = dataFilter.Disable<ActiveFeedFilter>();
        var context = new GtfsArchiveContext(feed, await repository.GetLookupCodesAsync(cancellationToken));
        using var archive = new MemoryStream();
        using (var zip = new ZipArchive(archive, ZipArchiveMode.Create, leaveOpen: true))
            foreach (var table in tables.OrderBy(table => table.ImportOrder))
                await table.ExportAsync(zip, context, cancellationToken);
        return archive.ToArray();
    }

    // Arsivi yerel dogrulayiciya gonderir ve cagriyi rapor ozetiyle dis servis loguna yazar; erisilemeyen dogrulayiciyi lokalize
    // hataya cevirir.
    private async Task<FeedValidationReportDto> SendAsync(
        byte[] archive, string fileName, CancellationToken cancellationToken = default)
    {
        var endpoint = ResolveLocalEndpoint();
        var report = new FeedValidationReportDto();
        try
        {
            await externalServiceLogger.TrackAsync(ValidationCall, async () =>
            {
                report = await PostAsync(endpoint, archive, fileName, cancellationToken);
                return JsonSerializer.Serialize(new
                {
                    report.ValidatorVersion,
                    ErrorCount = report.Errors.Sum(notice => notice.TotalNotices),
                    WarningCount = report.Warnings.Sum(notice => notice.TotalNotices)
                });
            }, cancellationToken);
            return report;
        }
        catch (HttpRequestException exception)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorUnavailable, innerException: exception);
        }
        catch (TaskCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorUnavailable, innerException: exception);
        }
    }

    // ZIP'i resmi validate-upload ucunun multipart file alanina koyup gonderir; bozuk arsiv icin gelen 422 cevabi da rapordur.
    private async Task<FeedValidationReportDto> PostAsync(
        Uri endpoint, byte[] archive, string fileName, CancellationToken cancellationToken = default)
    {
        using var client = clientFactory.CreateClient(GtfsValidationConsts.HttpClientName);
        using var fileContent = new ByteArrayContent(archive);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(GtfsValidationConsts.ZipMediaType);
        using var form = new MultipartFormDataContent();
        form.Add(fileContent, GtfsValidationConsts.UploadFieldName, Path.GetFileName(fileName));
        using var request = new HttpRequestMessage(HttpMethod.Post, endpoint) { Content = form };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(GtfsValidationConsts.JsonMediaType));
        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
        if (response.StatusCode != HttpStatusCode.UnprocessableEntity)
            response.EnsureSuccessStatusCode();
        return await ReadReportAsync(response, cancellationToken);
    }

    // Kullanici arsivinin gidecegi API hedefini loopback ile sinirlar.
    private Uri ResolveLocalEndpoint()
    {
        var baseUrl = options.Value.ValidatorBaseUrl;
        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var root) || !root.IsLoopback ||
            root.Scheme != Uri.UriSchemeHttp && root.Scheme != Uri.UriSchemeHttps)
            throw new BusinessException(FeedExceptionCodes.ValidatorNotConfigured);
        return new Uri(root, GtfsValidationConsts.ValidateUploadPath);
    }

    // Cevabi rapora cevirir; beklenen bicimde olmayan cevabi lokalize hataya cevirir.
    private static async Task<FeedValidationReportDto> ReadReportAsync(
        HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        await using var content = await response.Content.ReadAsStreamAsync(cancellationToken);
        try
        {
            using var document = await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken);
            return response.StatusCode == HttpStatusCode.UnprocessableEntity
                ? ToUnprocessableReport(document.RootElement)
                : ToReport(document.RootElement);
        }
        catch (Exception exception) when (exception is JsonException or InvalidOperationException or KeyNotFoundException)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorResponseInvalid, innerException: exception);
        }
    }

    // Surumu, dogrulama zamanini ve siddetine gore ayrilmis notice'lari okur.
    private static FeedValidationReportDto ToReport(JsonElement root)
    {
        var summary = root.GetProperty(GtfsValidationConsts.Json.Summary);
        var notices = root.GetProperty(GtfsValidationConsts.Json.Notices).EnumerateArray().Select(ToNotice)
            .ToLookup(notice => notice.Severity);
        return new FeedValidationReportDto
        {
            ValidatorVersion = Find(summary, GtfsValidationConsts.Json.ValidatorVersion, JsonValueKind.String)?.GetString(),
            ValidatedAt = DateTimeOffset.TryParse(
                Find(summary, GtfsValidationConsts.Json.ValidatedAt, JsonValueKind.String)?.GetString(),
                CultureInfo.InvariantCulture, out var validatedAt) ? validatedAt : null,
            Errors = notices[GtfsValidationConsts.ErrorSeverity].ToList(),
            Warnings = notices[GtfsValidationConsts.WarningSeverity].ToList(),
            Info = notices[GtfsValidationConsts.InfoSeverity].ToList()
        };
    }

    // Kural ihlalini kodu, siddeti, toplam sayisi ve ornekleriyle okur.
    private static FeedValidationNoticeDto ToNotice(JsonElement notice) => new()
    {
        Code = notice.GetProperty(GtfsValidationConsts.Json.Code).GetString()!,
        Severity = notice.GetProperty(GtfsValidationConsts.Json.Severity).GetString()!,
        TotalNotices = notice.GetProperty(GtfsValidationConsts.Json.TotalNotices).GetInt32(),
        Samples = notice.TryGetProperty(GtfsValidationConsts.Json.SampleNotices, out var samples)
            ? samples.EnumerateArray().Select(ToOccurrence).ToList()
            : []
    };

    // Ornegin dosya, alan ve satirini kurala gore degisen alan adlarindan (filename, childFilename, fieldName1) bulur; tum
    // baglami ayrintida saklar.
    private static FeedValidationOccurrenceDto ToOccurrence(JsonElement sample) => new()
    {
        FileName = Find(sample, GtfsValidationConsts.Json.FileNamePart, JsonValueKind.String)?.GetString(),
        FieldName = Find(sample, GtfsValidationConsts.Json.FieldNamePart, JsonValueKind.String)?.GetString(),
        CsvRowNumber = Find(sample, GtfsValidationConsts.Json.CsvRowNumberPart, JsonValueKind.Number)?.GetInt64(),
        Details = sample.Clone()
    };

    // Bozuk arsiv icin kanonik API'nin 422 cevabini tek hata notice'una cevirir.
    private static FeedValidationReportDto ToUnprocessableReport(JsonElement root) => new()
    {
        Errors =
        [
            new FeedValidationNoticeDto
            {
                Code = GtfsValidationConsts.UnprocessableFeedCode,
                Severity = GtfsValidationConsts.ErrorSeverity,
                TotalNotices = 1,
                Samples = [new FeedValidationOccurrenceDto { Details = root.Clone() }]
            }
        ]
    };

    // Adinda verilen parcayi buyuk-kucuk harf ayirmadan iceren ve beklenen turdeki ilk alani dondurur.
    private static JsonElement? Find(JsonElement source, string namePart, JsonValueKind kind)
        => source.EnumerateObject()
            .Where(property => property.Value.ValueKind == kind &&
                               property.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
            .Select(property => (JsonElement?)property.Value)
            .FirstOrDefault();
}
