using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Interfaces.Feeds;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Volo.Abp;

namespace Byd.GtfsManagement.GtfsDownloader;

// islevi: Verilen HTTP(S) adresinden GTFS ZIP'ini indirir.
// sistemdeki gorevi: Dis HTTP cagrisini ve indirilen boyutu Log modulune kaydeder.
public class GtfsFeedDownloader(
    IHttpClientFactory clientFactory,
    IExternalServiceLogger externalServiceLogger) : IGtfsFeedDownloader
{
    private static readonly ExternalServiceCall DownloadCall = new(
        FeedConsts.ProviderName,
        FeedConsts.MethodName,
        FeedConsts.RequestType);

    // Verilen kaynak adresinden ZIP'i indirir ve dis servis cagrisini kaydeder.
    public virtual async Task<byte[]> DownloadAsync(string sourceUrl, CancellationToken cancellationToken = default)
    {
        if (!Uri.TryCreate(sourceUrl, UriKind.Absolute, out var source) ||
            (source.Scheme != Uri.UriSchemeHttp && source.Scheme != Uri.UriSchemeHttps))
            throw new BusinessException(FeedExceptionCodes.SourceUrlInvalid);

        byte[] archive = [];
        await externalServiceLogger.TrackAsync(DownloadCall, async () =>
        {
            using var client = clientFactory.CreateClient(FeedConsts.HttpClientName);
            archive = await client.GetByteArrayAsync(source, cancellationToken);
            return archive.Length.ToString(CultureInfo.InvariantCulture);
        }, cancellationToken);
        return archive;
    }
}
