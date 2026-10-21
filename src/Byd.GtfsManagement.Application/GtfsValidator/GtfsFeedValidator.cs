using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Byd.FileManagement.Services.StoredFiles;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Mappers.Feeds;
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
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace Byd.GtfsManagement.GtfsValidator;

// islevi: Feed'i degistiren islemi calistirir; Feed'in guncel GTFS arsivini MobilityData dogrulayicisinin CLI'ina dogrulatir,
// urettigi raporu okur, raporu Feed'e yazar ve dondurur. Kayitli raporu JSON dosyasi olarak verir.
// sistemdeki gorevi: Dogrulamanin tek giris noktasidir. Feed'in guncel verisi veritabanina aktarilmadan once yuklenen ZIP, aktarildiktan
// sonra tablolarin kendisidir; tablolar ice aktarimla ayni DTO ve mapper'larla geri GTFS arsivine yazilir. Arsiv makineden disari
// cikmaz; CLI host'un yanindaki jar'dan calisir ve cagri log modulunun dis servis loguna yazilir.
// NEDEN MAPPERLY DEGIL (rapor okuma): sampleNotices alanlari kurala gore degisir; burada domain eslemesi degil, saglayici JSON
// protokolunun ayristirilmasi yapilir.
public class GtfsFeedValidator(
    IExternalServiceLogger externalServiceLogger,
    IStoredFileAppService storedFiles,
    IEnumerable<IGtfsImportExportAppService<GtfsArchiveContext>> tables,
    IFeedRepository repository,
    FeedManager feedManager,
    IDataFilter dataFilter,
    IDistributedEventBus distributedEventBus,
    IClock clock) : IGtfsFeedValidator, ITransientDependency
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

    // CLI'in raporu ayni cagrida doner; arka plan entegrasyonu dagitik olaya birakilir.
    private async Task<FeedValidationReportDto> ValidateArchiveAsync(
        Feed feed, CancellationToken cancellationToken = default)
        => await RunValidatorAsync(await GetArchiveAsync(feed, cancellationToken), cancellationToken);

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

    // Arsivi CLI'a dogrulatir ve cagriyi rapor ozetiyle dis servis loguna yazar.
    private async Task<FeedValidationReportDto> RunValidatorAsync(
        byte[] archive, CancellationToken cancellationToken = default)
    {
        var report = new FeedValidationReportDto();
        await externalServiceLogger.TrackAsync(ValidationCall, async () =>
        {
            report = await RunCliAsync(archive, cancellationToken);
            return JsonSerializer.Serialize(new
            {
                report.ValidatorVersion,
                ErrorCount = report.Errors.Sum(notice => notice.TotalNotices),
                WarningCount = report.Warnings.Sum(notice => notice.TotalNotices)
            });
        }, cancellationToken);
        return report;
    }

    // Arsivi gecici bir klasore yazar, CLI'in o klasore urettigi raporu okur ve klasoru her durumda siler. CLI rapor
    // uretmeden biterse (or: uyumsuz Java) dogrulayici erisilemez sayilir; hata ciktisi ic istisnada kalir.
    private static async Task<FeedValidationReportDto> RunCliAsync(
        byte[] archive, CancellationToken cancellationToken = default)
    {
        var workDirectory = Directory.CreateTempSubdirectory(GtfsValidationConsts.WorkDirectoryPrefix).FullName;
        try
        {
            var archivePath = Path.Combine(workDirectory, GtfsFileNames.Archive);
            var outputDirectory = Path.Combine(workDirectory, GtfsValidationConsts.OutputDirectoryName);
            await File.WriteAllBytesAsync(archivePath, archive, cancellationToken);
            var errorOutput = await ExecuteAsync(CreateStartInfo(archivePath, outputDirectory), cancellationToken);
            return File.Exists(Path.Combine(outputDirectory, GtfsValidationConsts.ReportJsonFileName))
                ? await ReadReportAsync(outputDirectory, cancellationToken)
                : throw new BusinessException(FeedExceptionCodes.ValidatorUnavailable,
                    innerException: new InvalidOperationException(errorOutput));
        }
        finally
        {
            Directory.Delete(workDirectory, recursive: true);
        }
    }

    // Java'yi JAVA_HOME'dan, yoksa PATH'ten bulur; CLI'i guncelleme kontrolu yapmadan calistirir. Host'un yaninda jar yoksa
    // dogrulayici yapilandirilmamis sayilir.
    private static ProcessStartInfo CreateStartInfo(string archivePath, string outputDirectory)
    {
        var cliPath = Path.Combine(AppContext.BaseDirectory, GtfsValidationConsts.CliDirectoryName,
            GtfsValidationConsts.CliFileName);
        if (!File.Exists(cliPath))
            throw new BusinessException(FeedExceptionCodes.ValidatorNotConfigured);
        var javaHome = Environment.GetEnvironmentVariable(GtfsValidationConsts.JavaHomeVariable);
        var java = string.IsNullOrWhiteSpace(javaHome)
            ? GtfsValidationConsts.JavaExecutable
            : Path.Combine(javaHome, GtfsValidationConsts.JavaBinDirectoryName, GtfsValidationConsts.JavaExecutable);
        return new ProcessStartInfo(java)
        {
            ArgumentList =
            {
                GtfsValidationConsts.Cli.Jar, cliPath,
                GtfsValidationConsts.Cli.Input, archivePath,
                GtfsValidationConsts.Cli.Output, outputDirectory,
                GtfsValidationConsts.Cli.SkipValidatorUpdate
            },
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
    }

    // CLI'i calistirip bitmesini bekler ve hata ciktisini doner; Java baslatilamaz ya da sure dolarsa dogrulayiciyi erisilemez
    // sayar. Iptal ve zaman asiminda surec agaciyla sonlandirilir.
    private static async Task<string> ExecuteAsync(
        ProcessStartInfo startInfo, CancellationToken cancellationToken = default)
    {
        using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeout.CancelAfter(GtfsValidationConsts.ValidationTimeout);
        using var process = StartProcess(startInfo);
        await using var kill = timeout.Token.Register(() => process.Kill(entireProcessTree: true));
        try
        {
            var errorOutput = process.StandardError.ReadToEndAsync(timeout.Token);
            await Task.WhenAll(process.StandardOutput.ReadToEndAsync(timeout.Token), errorOutput,
                process.WaitForExitAsync(timeout.Token));
            return await errorOutput;
        }
        catch (OperationCanceledException exception) when (!cancellationToken.IsCancellationRequested)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorUnavailable, innerException: exception);
        }
    }

    // Java bulunamazsa dogrulayiciyi erisilemez sayar.
    private static Process StartProcess(ProcessStartInfo startInfo)
    {
        try
        {
            return Process.Start(startInfo)!;
        }
        catch (Win32Exception exception)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorUnavailable, innerException: exception);
        }
    }

    // CLI'in yazdigi raporu ve calisma hatalarini okur. Bozuk arsiv gibi dogrulamanin tamamlanamadigi durumlar calisma hatasi
    // olarak gelir ve rapora hata olarak eklenir; beklenen bicimde olmayan cikti lokalize hataya cevrilir.
    private static async Task<FeedValidationReportDto> ReadReportAsync(
        string outputDirectory, CancellationToken cancellationToken = default)
    {
        try
        {
            using var report = await ParseAsync(
                Path.Combine(outputDirectory, GtfsValidationConsts.ReportJsonFileName), cancellationToken);
            using var systemErrors = await ParseAsync(
                Path.Combine(outputDirectory, GtfsValidationConsts.SystemErrorsJsonFileName), cancellationToken);
            var result = ToReport(report.RootElement);
            result.Errors.AddRange(systemErrors.RootElement.GetProperty(GtfsValidationConsts.Json.Notices)
                .EnumerateArray().Select(ToNotice));
            return result;
        }
        catch (Exception exception) when (exception is IOException or JsonException or InvalidOperationException
                                              or KeyNotFoundException)
        {
            throw new BusinessException(FeedExceptionCodes.ValidatorResponseInvalid, innerException: exception);
        }
    }

    // CLI'in yazdigi JSON dosyasini okur.
    private static async Task<JsonDocument> ParseAsync(string path, CancellationToken cancellationToken = default)
    {
        await using var content = File.OpenRead(path);
        return await JsonDocument.ParseAsync(content, cancellationToken: cancellationToken);
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

    // Adinda verilen parcayi buyuk-kucuk harf ayirmadan iceren ve beklenen turdeki ilk alani dondurur.
    private static JsonElement? Find(JsonElement source, string namePart, JsonValueKind kind)
        => source.EnumerateObject()
            .Where(property => property.Value.ValueKind == kind &&
                               property.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
            .Select(property => (JsonElement?)property.Value)
            .FirstOrDefault();
}
