using System;
using System.Threading.Tasks;
using NSubstitute;
using Byd.GtfsManagement.Etos.Feeds;
using Byd.GtfsManagement.Executors;
using Xunit;

namespace Byd.GtfsManagement.Handlers;

public class FeedIntegrationRequestedEventHandlerTests
{
    [Fact]
    public async Task HandleEventAsync_ForwardsEventScopeAndFeedId()
    {
        var request = new FeedIntegrationRequestedEto
        {
            TenantId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            FeedId = Guid.NewGuid()
        };
        var executor = CreateExecutor();
        var handler = new FeedIntegrationRequestedEventHandler(executor);

        await handler.HandleEventAsync(request);

        await executor.Received(1).IntegrateAsync(request.FeedId, request.TenantId, request.OrganizationId);
    }

    [Fact]
    public async Task HandleEventAsync_PropagatesIntegrationFailure()
    {
        var request = new FeedIntegrationRequestedEto
        {
            TenantId = Guid.NewGuid(),
            OrganizationId = Guid.NewGuid(),
            FeedId = Guid.NewGuid()
        };
        var executor = CreateExecutor();
        executor.IntegrateAsync(request.FeedId, request.TenantId, request.OrganizationId)
            .Returns(Task.FromException(new InvalidOperationException("integration failed")));
        var handler = new FeedIntegrationRequestedEventHandler(executor);

        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.HandleEventAsync(request));
    }

    private static FeedIntegrationExecutor CreateExecutor() => Substitute.For<FeedIntegrationExecutor>(
        null!, null!, null!, null!, null!, null!, null!, null!, null!, null!, null!);
}
