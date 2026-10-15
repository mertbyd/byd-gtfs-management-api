using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.GtfsMapper;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Managers;
using Byd.GtfsManagement.Mappers;
using Byd.GtfsManagement.Services;
using Volo.Abp.DependencyInjection;

namespace Byd.GtfsManagement;

// islevi: CRUD tabanina tablonun GTFS arsivinden ice, arsive disa aktarimini ekler.
// sistemdeki gorevi: Dosya beyanini DTO'daki GtfsFile'dan, CSV donusumunu GtfsCsvMapper'dan, DTO-entity donusumunu tablonun mevcut
// Mapperly mapper'indan alir; yaprak servis yalniz ImportDto tipini bildirir. Entegrasyon iscisi ve dogrulayici tablolari bu
// sozlesmeyle bulur. Arsiv uyeleri acik uygulanir; HTTP ucu ve CRUD yuzeyi degildir.
[ExposeServices(typeof(IGtfsImportExportAppService<GtfsArchiveContext>), IncludeDefaults = true)]
public abstract class GtfsImportExportAppServiceBase<
    TEntity, TCreateDto, TUpdateDto, TDto, TFilter,
    TCreateModel, TUpdateModel, TManager, TRepository, TMapper, TImportDto>
    : GtfsCrudAppServiceBase<
        TEntity, TCreateDto, TUpdateDto, TDto, TFilter, TCreateModel, TUpdateModel, TManager, TRepository, TMapper>,
        IGtfsImportExportAppService<GtfsArchiveContext>
    where TEntity : class, IGtfsEntity
    where TFilter : class, IFeedFilter, new()
    where TManager : GtfsFeedScopedManager<TEntity, TCreateModel, TUpdateModel>
    where TRepository : IExtendedRepository<TEntity>
    where TMapper : IGtfsCrudMapper<TEntity, TCreateDto, TUpdateDto, TDto, TCreateModel, TUpdateModel>,
        IGtfsArchiveMapper<TImportDto, TEntity>, new()
{
    // Tablonun dosya ve sira beyani; her kapali tip icin bir kez okunur.
    private static readonly GtfsFileAttribute GtfsFile = typeof(TImportDto).GetCustomAttribute<GtfsFileAttribute>()!;

    private IFeedRepository FeedRepository => LazyGetRequiredService<IFeedRepository>();

    // DTO'daki GtfsFile beyanindaki yazim sirasi.
    int IGtfsImportExportAppService<GtfsArchiveContext>.ImportOrder => GtfsFile.ImportOrder;

    // Tablonun GTFS dosyalarini okur, mevcut Mapperly ile entity'ye cevirir ve Feed'e toplu yazar; birden cok dosyadan
    // turetilen tablonun ayni kaydi bir kez yazilir.
    async Task IGtfsImportExportAppService<GtfsArchiveContext>.ImportAsync(
        ZipArchive archive, GtfsArchiveContext context, CancellationToken cancellationToken)
    {
        var keys = new GtfsKeyConverter(context);
        var records = GtfsFile.FileNames
            .SelectMany(name => GtfsCsvMapper.Map<TImportDto>(archive.GetEntry(name), keys))
            .Select(Mapper.Map);
        await FeedRepository.InsertRecordsAsync(context.Feed,
            GtfsFile.IsDerived ? records.DistinctBy(record => record.Id) : records, cancellationToken);
    }

    // Kayitlari mevcut Mapperly ile DTO'ya cevirip GTFS dosyasina yazar; once bu tabloya bakan yabanci anahtarlar icin
    // kayitlarin GTFS kimliklerini toplar.
    async Task IGtfsImportExportAppService<GtfsArchiveContext>.ExportAsync(
        ZipArchive archive, GtfsArchiveContext context, CancellationToken cancellationToken)
    {
        var records = FeedRepository.GetRecordsAsync<TEntity>(context.Feed.Id, cancellationToken);
        if (typeof(TEntity).IsAssignableTo(typeof(IHasGtfsId)))
            await foreach (var record in records)
                context.CollectId(record.Id, ((IHasGtfsId)record).GtfsId);
        if (GtfsFile.ExportFile is not { } fileName)
            return;
        await GtfsCsvMapper.Map(records.Select(Mapper.MapToImport), archive, fileName,
            new GtfsKeyConverter(context), cancellationToken);
    }
}
