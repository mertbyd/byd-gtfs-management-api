using System;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Dtos.Feeds;

namespace Byd.GtfsManagement.Interfaces.Feeds;

// islevi: Bir Feed'i degistiren islemi calistirir, ardindan Feed'in guncel GTFS verisini kanonik dogrulayicida kontrol edip
// raporu Feed'e yazar ve dondurur; kayitli raporu dosya olarak verir.
// sistemdeki gorevi: Dogrulamanin uygulamadaki tek giris noktasidir. Yukleme ile kayit ekleme, guncelleme ve silme ayni cagriyla
// sarilir; arsivi hazirlama, dogrulayici cagrisi, raporu okuma ve Feed'e yazma cagirana dusmez.
public interface IGtfsFeedValidator
{
    Task<FeedValidationReportDto> ValidateAsync<TResult>(
        Func<Task<TResult>> change, Func<TResult, Guid> feedIdOf, CancellationToken cancellationToken = default);

    Task<FeedFileDto> ExportReportAsync(Guid feedId, CancellationToken cancellationToken = default);

    Task<FeedFileDto> ExportArchiveAsync(Guid feedId, CancellationToken cancellationToken = default);
}
