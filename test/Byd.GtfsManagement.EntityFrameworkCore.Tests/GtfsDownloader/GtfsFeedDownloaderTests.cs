using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.GtfsDownloader;
using Byd.LogManagement.Dtos.ExternalServiceLogs;
using Byd.LogManagement.Interfaces.ExternalServiceLogs;
using Xunit;
using Volo.Abp;

namespace Byd.GtfsManagement.EntityFrameworkCore.GtfsDownloader;

public class GtfsFeedDownloaderTests
{
    [Fact]
    public async Task DownloadAsync_UsesTheSuppliedUrlForEveryRequest()
    {
        using var handler = new RecordingHandler();
        var clientFactory = Substitute.For<IHttpClientFactory>();
        clientFactory.CreateClient(FeedConsts.HttpClientName)
            .Returns(_ => new HttpClient(handler, disposeHandler: false));
        var logger = Substitute.For<IExternalServiceLogger>();
        logger.TrackAsync(Arg.Any<ExternalServiceCall>(), Arg.Any<Func<Task<string>>>(), Arg.Any<CancellationToken>())
            .Returns(call => call.ArgAt<Func<Task<string>>>(1)());
        var downloader = new GtfsFeedDownloader(clientFactory, logger);

        const string firstUrl = "http://localhost:5050/first.zip";
        const string secondUrl = "https://example.net/second.zip";
        var firstArchive = await downloader.DownloadAsync(firstUrl);
        var secondArchive = await downloader.DownloadAsync(secondUrl);

        Assert.Equal(firstUrl, Encoding.UTF8.GetString(firstArchive));
        Assert.Equal(secondUrl, Encoding.UTF8.GetString(secondArchive));
        Assert.Equal([firstUrl, secondUrl], handler.RequestedUrls);
    }

    [Theory]
    [InlineData("ftp://example.org/feed.zip")]
    [InlineData("not-a-url")]
    public async Task DownloadAsync_RejectsNonHttpUrlBeforeSending(string url)
    {
        using var handler = new RecordingHandler();
        var clientFactory = Substitute.For<IHttpClientFactory>();
        clientFactory.CreateClient(FeedConsts.HttpClientName)
            .Returns(_ => new HttpClient(handler, disposeHandler: false));
        var logger = Substitute.For<IExternalServiceLogger>();
        var downloader = new GtfsFeedDownloader(clientFactory, logger);

        var failure = await Assert.ThrowsAsync<BusinessException>(() => downloader.DownloadAsync(url));

        Assert.Equal(FeedExceptionCodes.SourceUrlInvalid, failure.Code);
        Assert.Empty(handler.RequestedUrls);
    }

    private sealed class RecordingHandler : HttpMessageHandler
    {
        public List<string> RequestedUrls { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var url = request.RequestUri!.AbsoluteUri;
            RequestedUrls.Add(url);
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Encoding.UTF8.GetBytes(url))
            });
        }
    }
}
