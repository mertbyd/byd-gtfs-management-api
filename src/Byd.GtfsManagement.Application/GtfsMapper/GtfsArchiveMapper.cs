using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.GtfsManagement.Services;
using Volo.Abp.DependencyInjection;

namespace Byd.GtfsManagement.GtfsMapper;

// islevi: Bir GTFS arsivinin tum dosyalarini tablolarin yazim sirasiyla Feed'e aktarir.
// sistemdeki gorevi: Arsiv ile Feed tablolari arasindaki tek giris noktasidir; tablo listesini ve sirasini burada dolasir, her
// tablonun dosya ve sira beyanini o tablonun kendi servisi bilir. Lookup cevirisi her calisma icin bir kez okunur.
public class GtfsArchiveMapper(
    IEnumerable<IGtfsImportExportAppService<GtfsArchiveContext>> tables,
    IFeedRepository repository) : ITransientDependency
{
    // ZIP'in tum GTFS dosyalarini tablolarin yazim sirasiyla Feed'e aktarir.
    public virtual async Task ImportAsync(Feed feed, byte[] archive, CancellationToken cancellationToken = default)
    {
        using var zip = new ZipArchive(new MemoryStream(archive, writable: false), ZipArchiveMode.Read);
        var context = new GtfsArchiveContext(feed, await repository.GetLookupCodesAsync(cancellationToken));
        foreach (var table in tables.OrderBy(table => table.ImportOrder))
            await table.ImportAsync(zip, context, cancellationToken);
    }
}
