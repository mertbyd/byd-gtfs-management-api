using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace Byd.GtfsManagement.Services;

// islevi: Bir GTFS tablosunun arsivden ice ve arsive disa aktarim sozlesmesini bildirir.
// sistemdeki gorevi: Ortak import/export tabani uygular; entegrasyon iscisi ve dogrulayici tablolari tip, dosya ve mapper
// bilmeden ImportOrder sirasiyla calistirir. Arsiv baglami uygulama katmaninda oldugu icin tip parametresiyle gelir; HTTP ucu degildir.
public interface IGtfsImportExportAppService<TContext>
{
    // DTO'daki GtfsFile beyanindaki yazim sirasi.
    int ImportOrder { get; }

    Task ImportAsync(ZipArchive archive, TContext context, CancellationToken cancellationToken = default);

    Task ExportAsync(ZipArchive archive, TContext context, CancellationToken cancellationToken = default);
}
