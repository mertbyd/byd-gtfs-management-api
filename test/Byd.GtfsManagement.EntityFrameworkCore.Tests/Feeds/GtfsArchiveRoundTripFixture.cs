using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.GtfsMapper;
using Byd.GtfsManagement.Interfaces;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Byd.GtfsManagement.Services;
using Byd.GtfsManagement.Services.FeedInfos;
using Byd.GtfsManagement.Services.Levels;
using Byd.GtfsManagement.Services.Pathways;
using Byd.GtfsManagement.Services.Stops;
using Volo.Abp.DependencyInjection;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// islevi: Uretimin CSV ve Mapperly akislarini gercek tablo AppService'leriyle calistiran test arsivini kurar.
// sistemdeki gorevi: Yalniz kalici yazma/okuma seam'i taklit edilir; import, export, anahtar cevirisi ve siralama gercektir.
internal sealed class GtfsArchiveRoundTripFixture : IDisposable
{
    private readonly Dictionary<Guid, List<IGtfsEntity>> _records = [];
    private readonly ServiceProvider _services;
    private readonly IGtfsImportExportAppService<GtfsArchiveContext>[] _tables;
    private readonly IFeedRepository _repository = Substitute.For<IFeedRepository>();
    private readonly List<GtfsLookupCodeModel> _codes =
    [
        new() { TableName = LocationTypeConsts.TableName, Code = 0, Id = 100 },
        new() { TableName = LocationTypeConsts.TableName, Code = 1, Id = 101 }
    ];

    // Gercek tablo servislerine yalniz repository bagimliligini saglar.
    public GtfsArchiveRoundTripFixture()
    {
        _repository.GetLookupCodesAsync(Arg.Any<CancellationToken>()).Returns(_codes);
        _repository.InsertRecordsAsync(Arg.Any<Feed>(), Arg.Any<IEnumerable<IGtfsEntity>>(), Arg.Any<CancellationToken>())
            .Returns(call => RecordImport(call.ArgAt<Feed>(0), call.ArgAt<IEnumerable<IGtfsEntity>>(1)));
        BindRecords<Level>();
        BindRecords<Stop>();
        BindRecords<Pathway>();
        BindRecords<FeedInfo>();
        _services = new ServiceCollection().AddSingleton(_repository).BuildServiceProvider();
        var lazy = new AbpLazyServiceProvider(_services);
        // Bilerek ters sira verilir; arsiv mapper'i DTO'daki ImportOrder'i uygulamalidir.
        _tables =
        [
            new PathwayAppService { LazyServiceProvider = lazy },
            new StopAppService { LazyServiceProvider = lazy },
            new LevelAppService { LazyServiceProvider = lazy },
            new FeedInfoAppService { LazyServiceProvider = lazy }
        ];
    }

    // Kaydedilmis satirlari yalniz verilen surumden getirir.
    public TEntity[] Records<TEntity>(Guid versionId) where TEntity : class, IGtfsEntity
        => _records.GetValueOrDefault(versionId, []).OfType<TEntity>().ToArray();

    // Tum GTFS tablolarini uretim arsiv mapper'inin belirledigi sirada ice alir.
    public Task ImportAsync(Feed version, byte[] archive)
        => new GtfsArchiveMapper(_tables, _repository).ImportAsync(version, archive);

    // Uretim tablo export sozlesmesini referans dosyalari once olacak sirayla kullanir.
    public async Task<byte[]> ExportAsync(Feed version)
    {
        using var stream = new MemoryStream();
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
        {
            var context = new GtfsArchiveContext(version, _codes);
            foreach (var table in _tables.OrderBy(table => table.ImportOrder))
                await table.ExportAsync(archive, context);
        }
        return stream.ToArray();
    }

    // Ornek GTFS dosyalarini gercek UTF-8 ZIP'e yazar.
    public static byte[] CreateArchive(params (string FileName, string Csv)[] files)
    {
        using var stream = new MemoryStream();
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var (fileName, csv) in files)
            {
                using var writer = new StreamWriter(archive.CreateEntry(fileName).Open(), new UTF8Encoding(false));
                writer.Write(csv);
            }
        }
        return stream.ToArray();
    }

    // Lazy import enumerasyonunu gercek mapper'in urettigi kayitlarla saklar.
    private Task RecordImport(Feed version, IEnumerable<IGtfsEntity> rows)
    {
        if (!_records.TryGetValue(version.Id, out var saved))
            _records[version.Id] = saved = [];
        saved.AddRange(rows);
        return Task.CompletedTask;
    }

    // Tipli export akisi hangi surum isteniyorsa o surumun kayitlarini kullanir.
    private void BindRecords<TEntity>() where TEntity : class, IGtfsEntity
        => _repository.GetRecordsAsync<TEntity>(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(call => ReadAsync(Records<TEntity>(call.ArgAt<Guid>(0)), call.ArgAt<CancellationToken>(1)));

    // Export'un uretimde tukettigi async stream sozlesmesini testte uygular.
    private static async IAsyncEnumerable<TEntity> ReadAsync<TEntity>(IEnumerable<TEntity> records,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
        foreach (var record in records)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return record;
        }
    }

    // Teste ait servis saglayicisini kapatir.
    public void Dispose() => _services.Dispose();
}
