using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Interfaces;
using Xunit;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// Exercises the production filters and database constraints with two tenants and organizations.
public class ActiveFeedFilterTests
{
    [Fact]
    public async Task FeedEnums_AreStoredAsNames()
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        await using var context = fixture.CreateContext();
        var feedType = context.Model.FindEntityType(typeof(Feed))!;

        Assert.Equal("Upload", feedType.FindProperty(nameof(Feed.SourceType))!
            .GetTypeMapping().Converter!.ConvertToProvider(FeedSourceType.Upload));
        Assert.Equal("Integrated", feedType.FindProperty(nameof(Feed.Status))!
            .GetTypeMapping().Converter!.ConvertToProvider(FeedStatus.Integrated));
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    public async Task Enabled_ReturnsOnlyCurrentOrganizationsActiveFeed(int tenantIndex, int organizationIndex)
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(fixture.Tenants[tenantIndex], fixture.Organizations[organizationIndex]);
        await using var context = fixture.CreateContext();
        var expected = Assert.Single(fixture.GetFeeds(tenantIndex, organizationIndex)).Active;

        Assert.Equal(8, await context.Stops.IgnoreQueryFilters().CountAsync());
        Assert.Equal([expected.StopId], await context.Stops.Select(stop => stop.Id).ToArrayAsync());
        Assert.Equal([expected.LevelId], await context.Levels.Select(level => level.Id).ToArrayAsync());
        Assert.Equal([expected.AgencyId], await context.Agencies.Select(agency => agency.Id).ToArrayAsync());
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    public async Task Disabled_ShowsPassiveFeedOnlyWithinCurrentScope(int tenantIndex, int organizationIndex)
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(fixture.Tenants[tenantIndex], fixture.Organizations[organizationIndex]);
        await using var context = fixture.CreateContext();
        var feeds = Assert.Single(fixture.GetFeeds(tenantIndex, organizationIndex));

        using (fixture.DataFilter.Disable<ActiveFeedFilter>())
        {
            Assert.Equal(new[] { feeds.Active.StopId, feeds.Inactive.StopId }.Order(),
                await context.Stops.Select(stop => stop.Id).OrderBy(id => id).ToArrayAsync());
            Assert.Equal(2, await context.Feeds.CountAsync());
        }
        Assert.Equal([feeds.Active.StopId], await context.Stops.Select(stop => stop.Id).ToArrayAsync());
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData(0, null)]
    public async Task MissingScope_HidesRowsEvenWhenActiveFeedFilterIsDisabled(int? tenantIndex, int? organizationIndex)
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(tenantIndex is null ? null : fixture.Tenants[tenantIndex.Value],
            organizationIndex is null ? null : fixture.Organizations[organizationIndex.Value]);
        await using var context = fixture.CreateContext();

        using (fixture.DataFilter.Disable<ActiveFeedFilter>())
        {
            Assert.Empty(await context.Stops.ToArrayAsync());
        }
    }

    [Fact]
    public async Task ActivationSwitch_ChangesVisibleRowsWithoutRewritingGtfsRows()
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(fixture.Tenants[0], fixture.Organizations[0]);
        await using var context = fixture.CreateContext();
        var feeds = Assert.Single(fixture.GetFeeds(0, 0));
        var rowsBefore = await context.Stops.IgnoreQueryFilters()
            .Select(stop => new { stop.Id, stop.FeedId }).OrderBy(stop => stop.Id).ToArrayAsync();

        using (fixture.DataFilter.Disable<ActiveFeedFilter>())
        {
            var oldFeed = await context.Feeds.SingleAsync(feed => feed.Id == feeds.Active.FeedId);
            var newFeed = await context.Feeds.SingleAsync(feed => feed.Id == feeds.Inactive.FeedId);
            context.Entry(oldFeed).Property(feed => feed.IsActive).CurrentValue = false;
            await context.SaveChangesOnDbContextAsync(true);
            context.Entry(newFeed).Property(feed => feed.IsActive).CurrentValue = true;
            await context.SaveChangesOnDbContextAsync(true);
        }

        Assert.Equal([feeds.Inactive.StopId], await context.Stops.Select(stop => stop.Id).ToArrayAsync());
        Assert.Equal(rowsBefore, await context.Stops.IgnoreQueryFilters()
            .Select(stop => new { stop.Id, stop.FeedId }).OrderBy(stop => stop.Id).ToArrayAsync());
    }

    [Fact]
    public async Task Database_RejectsSecondActiveFeedInOrganization()
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(fixture.Tenants[0], fixture.Organizations[0]);
        await using var context = fixture.CreateContext();
        var feeds = Assert.Single(fixture.GetFeeds(0, 0));
        using var history = fixture.DataFilter.Disable<ActiveFeedFilter>();
        var second = await context.Feeds.SingleAsync(feed => feed.Id == feeds.Inactive.FeedId);
        context.Entry(second).Property(feed => feed.IsActive).CurrentValue = true;

        await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesOnDbContextAsync(true));
    }

    [Fact]
    public async Task StopCannotReferenceLevelFromAnotherFeed()
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        fixture.SetScope(fixture.Tenants[0], fixture.Organizations[0]);
        var feed = Assert.Single(fixture.GetFeeds(0, 0));
        await using var context = fixture.CreateContext();
        var stop = new Stop(Guid.NewGuid(), feed.Active.FeedId);
        context.Stops.Add(stop);
        context.Entry(stop).Property(value => value.GtfsStopId).CurrentValue = "cross-feed";
        context.Entry(stop).Property(value => value.LevelId).CurrentValue = feed.Inactive.LevelId;

        var failure = await Assert.ThrowsAsync<DbUpdateException>(() => context.SaveChangesOnDbContextAsync(true));
        var sqliteFailure = Assert.IsType<SqliteException>(failure.InnerException);
        Assert.Equal(787, sqliteFailure.SqliteExtendedErrorCode);
    }

    [Fact]
    public async Task EveryFeedScopedEntity_HasFeedForeignKeyAndQueryFilter()
    {
        await using var fixture = new ActiveFeedFilterFixture();
        await fixture.InitializeAsync();
        await using var context = fixture.CreateContext();
        var types = context.Model.GetEntityTypes()
            .Where(type => typeof(IGtfsEntity).IsAssignableFrom(type.ClrType)).ToArray();

        Assert.NotEmpty(types);
        Assert.All(types, type =>
        {
            Assert.NotEmpty(type.GetDeclaredQueryFilters());
            Assert.NotNull(type.FindProperty(nameof(Stop.FeedId)));
            Assert.Contains(type.GetForeignKeys(), foreignKey =>
                foreignKey.PrincipalEntityType.ClrType == typeof(Feed) &&
                foreignKey.Properties.Count == 1 &&
                foreignKey.Properties[0].Name == nameof(Stop.FeedId));
            Assert.Null(type.FindProperty(nameof(Feed.TenantId)));
            Assert.Null(type.FindProperty(nameof(Feed.OrganizationId)));
            Assert.Null(type.FindProperty(nameof(Feed.IsActive)));
        });
    }
}
