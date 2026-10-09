using Microsoft.EntityFrameworkCore;
using Byd.CoreExtensions.EntityFrameworkCore;
using Byd.CoreExtensions.Extensions;
using Byd.GtfsManagement.Entities.Agencies;
using Byd.GtfsManagement.Entities.Areas;
using Byd.GtfsManagement.Entities.Attributions;
using Byd.GtfsManagement.Entities.BookingRules;
using Byd.GtfsManagement.Entities.CalendarDates;
using Byd.GtfsManagement.Entities.Calendars;
using Byd.GtfsManagement.Entities.FareAttributes;
using Byd.GtfsManagement.Entities.FareLegJoinRules;
using Byd.GtfsManagement.Entities.FareLegRules;
using Byd.GtfsManagement.Entities.FareMedias;
using Byd.GtfsManagement.Entities.FareProducts;
using Byd.GtfsManagement.Entities.FareRules;
using Byd.GtfsManagement.Entities.FareTransferRules;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.Frequencies;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Entities.LocationGroupStops;
using Byd.GtfsManagement.Entities.LocationGroups;
using Byd.GtfsManagement.Entities.Lookup.BikesAlloweds;
using Byd.GtfsManagement.Entities.Lookup.CarsAlloweds;
using Byd.GtfsManagement.Entities.Lookup.CemvSupports;
using Byd.GtfsManagement.Entities.Lookup.ContinuousPickupDropOffs;
using Byd.GtfsManagement.Entities.Lookup.ExceptionTypes;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Lookup.PickupDropOffTypes;
using Byd.GtfsManagement.Entities.Lookup.RouteTypes;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Byd.GtfsManagement.Entities.Lookup.TripDirections;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Entities.Networks;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.RiderCategories;
using Byd.GtfsManagement.Entities.RouteNetworks;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Services;
using Byd.GtfsManagement.Entities.Shapes;
using Byd.GtfsManagement.Entities.StopAreas;
using Byd.GtfsManagement.Entities.StopTimes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Timeframes;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Entities.Translations;
using Byd.GtfsManagement.Entities.Trips;
using Volo.Abp.Data;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS tablolarinin EF Core DbContext'idir.
// sistemdeki gorevi: Core'un tenant ve aktiflik filtrelerini tasiyan tabandan turer; tablo kurallari konfigurasyon siniflarindan uygulanir.
[ConnectionStringName(GtfsManagementDbProperties.ConnectionStringName)]
public class GtfsManagementDbContext : BydDbContext<GtfsManagementDbContext>, IGtfsManagementDbContext
{
    public DbSet<Agency> Agencies { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<CemvSupport> CemvSupports { get; set; }
    public DbSet<LocationType> LocationTypes { get; set; }
    public DbSet<WheelchairBoarding> WheelchairBoardings { get; set; }
    public DbSet<Stop> Stops { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Calendar> Calendars { get; set; }
    public DbSet<CalendarDate> CalendarDates { get; set; }
    public DbSet<ExceptionType> ExceptionTypes { get; set; }
    public DbSet<RouteType> RouteTypes { get; set; }
    public DbSet<ContinuousPickupDropOff> ContinuousPickupDropOffs { get; set; }
    public DbSet<PickupDropOffType> PickupDropOffTypes { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<StopTime> StopTimes { get; set; }
    public DbSet<Frequency> Frequencies { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<TripDirection> TripDirections { get; set; }
    public DbSet<BikesAllowed> BikesAlloweds { get; set; }
    public DbSet<CarsAllowed> CarsAlloweds { get; set; }
    public DbSet<StopAccess> StopAccesses { get; set; }
    public DbSet<Shape> Shapes { get; set; } = default!;
    public DbSet<FeedInfo> FeedInfos { get; set; } = default!;
    public DbSet<Level> Levels { get; set; } = default!;
    public DbSet<Pathway> Pathways { get; set; } = default!;
    public DbSet<FareAttribute> FareAttributes { get; set; } = default!;
    public DbSet<FareRule> FareRules { get; set; } = default!;
    public DbSet<Translation> Translations { get; set; } = default!;
    public DbSet<Attribution> Attributions { get; set; } = default!;
    public DbSet<Area> Areas { get; set; } = default!;
    public DbSet<Timeframe> Timeframes { get; set; } = default!;
    public DbSet<RiderCategory> RiderCategories { get; set; } = default!;
    public DbSet<FareMedia> FareMedias { get; set; } = default!;
    public DbSet<FareProduct> FareProducts { get; set; } = default!;
    public DbSet<FareLegRule> FareLegRules { get; set; } = default!;
    public DbSet<FareLegJoinRule> FareLegJoinRules { get; set; } = default!;
    public DbSet<FareTransferRule> FareTransferRules { get; set; } = default!;
    public DbSet<StopArea> StopAreas { get; set; } = default!;
    public DbSet<Network> Networks { get; set; } = default!;
    public DbSet<RouteNetwork> RouteNetworks { get; set; } = default!;
    public DbSet<LocationGroup> LocationGroups { get; set; } = default!;
    public DbSet<LocationGroupStop> LocationGroupStops { get; set; } = default!;
    public DbSet<BookingRule> BookingRules { get; set; } = default!;

    public GtfsManagementDbContext(DbContextOptions<GtfsManagementDbContext> options)
        : base(options)
    {
    }

    // Taban filtrelerini kurar, konfigurasyon siniflarini uygular ve kolon adlarini snake_case yapar.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(GtfsManagementDbContext).Assembly);

        builder.ConfigureGtfsManagement();

        builder.ApplySnakeCaseNaming();
    }
}
