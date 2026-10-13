using Microsoft.EntityFrameworkCore;
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
using Volo.Abp.EntityFrameworkCore;

namespace Byd.GtfsManagement.EntityFrameworkCore;

// islevi: GTFS DbContext sozlesmesini tanimlar.
// sistemdeki gorevi: Modulun baglanti dizesi adini EF katmanina bildirir.
[ConnectionStringName(GtfsManagementDbProperties.ConnectionStringName)]
public interface IGtfsManagementDbContext : IEfCoreDbContext
{
    DbSet<Agency> Agencies { get; }
    DbSet<Feed> Feeds { get; }
    DbSet<CemvSupport> CemvSupports { get; }
    DbSet<LocationType> LocationTypes { get; }
    DbSet<WheelchairBoarding> WheelchairBoardings { get; }
    DbSet<Stop> Stops { get; }
    DbSet<Route> Routes { get; }
    DbSet<Service> Services { get; }
    DbSet<Calendar> Calendars { get; }
    DbSet<CalendarDate> CalendarDates { get; }
    DbSet<ExceptionType> ExceptionTypes { get; }
    DbSet<RouteType> RouteTypes { get; }
    DbSet<ContinuousPickupDropOff> ContinuousPickupDropOffs { get; }
    DbSet<PickupDropOffType> PickupDropOffTypes { get; }
    DbSet<Trip> Trips { get; }
    DbSet<StopTime> StopTimes { get; }
    DbSet<Frequency> Frequencies { get; }
    DbSet<Transfer> Transfers { get; }
    DbSet<TripDirection> TripDirections { get; }
    DbSet<BikesAllowed> BikesAlloweds { get; }
    DbSet<CarsAllowed> CarsAlloweds { get; }
    DbSet<StopAccess> StopAccesses { get; }
    DbSet<Shape> Shapes { get; }
    DbSet<FeedInfo> FeedInfos { get; }
    DbSet<Level> Levels { get; }
    DbSet<Pathway> Pathways { get; }
    DbSet<FareAttribute> FareAttributes { get; }
    DbSet<FareRule> FareRules { get; }
    DbSet<Translation> Translations { get; }
    DbSet<Attribution> Attributions { get; }
    DbSet<Area> Areas { get; }
    DbSet<Timeframe> Timeframes { get; }
    DbSet<RiderCategory> RiderCategories { get; }
    DbSet<FareMedia> FareMedias { get; }
    DbSet<FareProduct> FareProducts { get; }
    DbSet<FareLegRule> FareLegRules { get; }
    DbSet<FareLegJoinRule> FareLegJoinRules { get; }
    DbSet<FareTransferRule> FareTransferRules { get; }
    DbSet<StopArea> StopAreas { get; }
    DbSet<Network> Networks { get; }
    DbSet<RouteNetwork> RouteNetworks { get; }
    DbSet<LocationGroup> LocationGroups { get; }
    DbSet<LocationGroupStop> LocationGroupStops { get; }
    DbSet<BookingRule> BookingRules { get; }
}
