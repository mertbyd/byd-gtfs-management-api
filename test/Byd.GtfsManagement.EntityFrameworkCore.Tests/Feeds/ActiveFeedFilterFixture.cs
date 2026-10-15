using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Feeds;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// islevi: Gercek GTFS EF modelini SQLite uzerinde iki tenant ve ikiser organizasyonla kurar.
// sistemdeki gorevi: Kapsam kimliklerini sahte erisimcilerden, filtre durumunu gercek ABP IDataFilter'dan alir.
internal sealed class ActiveFeedFilterFixture : IAsyncDisposable
{
    private readonly SqliteConnection _connection = new("Data Source=:memory:;Foreign Keys=True");
    private readonly ServiceProvider _services;
    private readonly DbContextOptions<GtfsManagementDbContext> _options;
    private Guid? _tenantId;
    private Guid? _organizationId;

    public Guid[] Tenants { get; } = [Guid.NewGuid(), Guid.NewGuid()];
    // Ayni organizasyon kimlikleri iki tenant'ta da kullanilir; sadece organizasyonu filtrelemek testi geciremez.
    public Guid[] Organizations { get; } = [Guid.NewGuid(), Guid.NewGuid()];
    public List<FeedTestOrganization> OrganizationsWithFeeds { get; } = [];
    public IDataFilter DataFilter => _services.GetRequiredService<IDataFilter>();

    // Host baslatmadan EF'nin gercek filtre ve scope servislerini baglar.
    public ActiveFeedFilterFixture()
    {
        var tenant = Substitute.For<ICurrentTenant>();
        tenant.Id.Returns(_ => _tenantId);
        var organization = Substitute.For<ICurrentOrganization>();
        organization.Id.Returns(_ => _organizationId);
        var clock = Substitute.For<IClock>();
        clock.Normalize(Arg.Any<DateTime>()).Returns(call => call.Arg<DateTime>());
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddSingleton(tenant);
        services.AddSingleton(organization);
        services.AddSingleton(clock);
        services.AddSingleton<IDataFilter, DataFilter>();
        services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
        _services = services.BuildServiceProvider();
        _options = new DbContextOptionsBuilder<GtfsManagementDbContext>().UseSqlite(_connection).Options;
    }

    // Her test icin izole veritabanini ve kapsam matrisini olusturur.
    public async Task InitializeAsync()
    {
        await _connection.OpenAsync();
        await using var context = CreateContext();
        await context.Database.EnsureCreatedAsync();
        foreach (var tenantId in Tenants)
        {
            foreach (var organizationId in Organizations)
            {
                await AddFeedAsync(context, tenantId, organizationId);
            }
        }
    }

    // Her context ayni gercek modeli kullanir; kapsam degerleri sorgu aninda okunur.
    public GtfsManagementDbContext CreateContext() => new(_options)
    {
        LazyServiceProvider = new AbpLazyServiceProvider(_services)
    };

    // Kapsam degisimini modelin yeniden kurulmasindan bagimsiz olarak uygular.
    public void SetScope(Guid? tenantId, Guid? organizationId)
    {
        _tenantId = tenantId;
        _organizationId = organizationId;
    }

    // Yalniz testte beklenen kapsam kimlikleriyle kurulan Feed'leri verir.
    public FeedTestOrganization[] GetFeeds(int tenantIndex, int organizationIndex) => OrganizationsWithFeeds
        .Where(item => item.TenantId == Tenants[tenantIndex] && item.OrganizationId == Organizations[organizationIndex])
        .ToArray();

    // Ayni organizasyona biri aktif, biri pasif iki bagimsiz Feed yazar.
    private async Task AddFeedAsync(GtfsManagementDbContext context, Guid tenantId, Guid organizationId)
    {
        var active = AddFeed(context, tenantId, organizationId, "Uploaded ZIP", true);
        var inactive = AddFeed(context, tenantId, organizationId, "Downloaded ZIP", false);
        await context.SaveChangesOnDbContextAsync(true);
        OrganizationsWithFeeds.Add(new FeedTestOrganization(tenantId, organizationId, active, inactive));
    }

    // EF test verisini internal setter'lari acmadan hazirlar; uygulama handler ve audit akislarini calistirmaz.
    private static FeedTestFeed AddFeed(GtfsManagementDbContext context, Guid tenantId,
        Guid organizationId, string name, bool isActive)
    {
        var feed = new Feed(Guid.NewGuid(), tenantId, organizationId, name, FeedSourceType.Upload, null);
        context.Feeds.Add(feed);
        context.Entry(feed).Property(value => value.IsActive).CurrentValue = isActive;
        context.Entry(feed).Property(value => value.StoredFileId).CurrentValue = Guid.NewGuid();
        context.Entry(feed).Property(value => value.StoredFileName).CurrentValue = "network.zip";
        context.Entry(feed).Property(value => value.Status).CurrentValue = FeedStatus.Integrated;
        var level = new Level(Guid.NewGuid(), feed.Id, "ground");
        context.Levels.Add(level);
        var stop = new Stop(Guid.NewGuid(), feed.Id);
        context.Stops.Add(stop);
        context.Entry(stop).Property(value => value.GtfsStopId).CurrentValue = "central";
        context.Entry(stop).Property(value => value.Name).CurrentValue = "Central station";
        context.Entry(stop).Property(value => value.LevelId).CurrentValue = level.Id;
        var agency = new Agency(Guid.NewGuid(), feed.Id);
        context.Agencies.Add(agency);
        context.Entry(agency).Property(value => value.Name).CurrentValue = "City transit";
        context.Entry(agency).Property(value => value.Url).CurrentValue = "https://example.org";
        context.Entry(agency).Property(value => value.Timezone).CurrentValue = "Europe/Vilnius";
        return new FeedTestFeed(feed.Id, stop.Id, level.Id, agency.Id);
    }

    // Teste ait acik baglanti ve servis saglayicisini serbest birakir.
    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _services.DisposeAsync();
    }
}
