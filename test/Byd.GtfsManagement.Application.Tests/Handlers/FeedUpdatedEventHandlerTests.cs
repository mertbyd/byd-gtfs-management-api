using System;
using System.Threading.Tasks;
using NSubstitute;
using Byd.CoreExtensions.Interfaces;
using Byd.CoreExtensions.Settings;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Stops;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Xunit;

namespace Byd.GtfsManagement.Handlers;

public class FeedUpdatedEventHandlerTests
{
    private readonly IStopRepository _stopRepository = Substitute.For<IStopRepository>();
    private readonly IRouteRepository _routeRepository = Substitute.For<IRouteRepository>();
    private readonly ISettingProvider _settingProvider = Substitute.For<ISettingProvider>();

    [Fact]
    public async Task HandleEventAsync_ReplacesStopAndRouteIndexesOfActiveFeed()
    {
        var feed = CreateFeed(isActive: true);
        EnableElasticsearch("true");

        await CreateHandler().HandleEventAsync(new EntityUpdatedEventData<Feed>(feed));

        await _stopRepository.Received(1).ReplaceSearchIndexAsync(feed.Id);
        await _routeRepository.Received(1).ReplaceSearchIndexAsync(feed.Id);
    }

    [Fact]
    public async Task HandleEventAsync_LeavesIndexesOfInactiveFeed()
    {
        EnableElasticsearch("true");

        await CreateHandler().HandleEventAsync(new EntityUpdatedEventData<Feed>(CreateFeed(isActive: false)));

        await _stopRepository.DidNotReceiveWithAnyArgs().ReplaceSearchIndexAsync(default);
        await _routeRepository.DidNotReceiveWithAnyArgs().ReplaceSearchIndexAsync(default);
    }

    [Fact]
    public async Task HandleEventAsync_LeavesIndexesWhenElasticsearchIsDisabled()
    {
        EnableElasticsearch("false");

        await CreateHandler().HandleEventAsync(new EntityUpdatedEventData<Feed>(CreateFeed(isActive: true)));

        await _stopRepository.DidNotReceiveWithAnyArgs().ReplaceSearchIndexAsync(default);
        await _routeRepository.DidNotReceiveWithAnyArgs().ReplaceSearchIndexAsync(default);
    }

    private FeedUpdatedEventHandler CreateHandler() => new(
        _stopRepository,
        _routeRepository,
        _settingProvider,
        Substitute.For<ICurrentTenant>(),
        Substitute.For<ICurrentOrganization>());

    private void EnableElasticsearch(string value)
        => _settingProvider.GetOrNullAsync(CoreExtensionsSettings.Elasticsearch.IsEnabled).Returns(value);

    private static Feed CreateFeed(bool isActive)
    {
        var feed = new Feed(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "feed", FeedSourceType.Upload, null);
        typeof(Feed).GetProperty(nameof(Feed.IsActive))!.SetValue(feed, isActive);
        return feed;
    }
}
