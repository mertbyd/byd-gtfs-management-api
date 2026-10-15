using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using Byd.GtfsManagement.Entities.Lookup.LocationTypes;
using Byd.GtfsManagement.Entities.Lookup.StopAccesses;
using Byd.GtfsManagement.Entities.Lookup.WheelchairBoardings;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Interfaces.Pathways;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Interfaces.Transfers;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Managers.Feeds;
using Byd.GtfsManagement.Managers.Journeys;
using Byd.GtfsManagement.Managers.Levels;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Models.Journeys;
using Byd.GtfsManagement.Models.StopTimes;
using Byd.GtfsManagement.Models.Stops;
using Volo.Abp.Domain.Repositories;

namespace Byd.GtfsManagement.Journeys;

// islevi: Gercek JourneyManager icin kapsamda gorunen duraklar ve servis gunune bagli ziyaretleri kurar.
// sistemdeki gorevi: Repository'ler yalniz veriyi saglar; yurume, aktarma, binis ve gece yarisi kurallari uretim Manager'indadir.
internal sealed class JourneyPlanningFixture
{
    private readonly List<Stop> _stops = [];
    private readonly List<StopPlaceModel> _places = [];
    private readonly List<Route> _routes = [];
    private readonly List<Trip> _trips = [];
    private readonly List<Pathway> _pathways = [];
    private readonly List<Transfer> _transfers = [];
    private readonly Dictionary<DateOnly, List<Guid>> _dailyServices = [];
    private readonly Dictionary<Guid, List<StopVisitModel>> _visits = [];
    private readonly ITripRepository _tripRepository = Substitute.For<ITripRepository>();
    private readonly IRouteRepository _routeRepository = Substitute.For<IRouteRepository>();
    private readonly IPathwayRepository _pathwayRepository = Substitute.For<IPathwayRepository>();
    private readonly ITransferRepository _transferRepository = Substitute.For<ITransferRepository>();

    public Guid TenantId { get; } = Guid.NewGuid();
    public DateOnly Date { get; } = new(2026, 10, 14);
    public IStopRepository StopRepository { get; } = Substitute.For<IStopRepository>();
    public IServiceRepository ServiceRepository { get; } = Substitute.For<IServiceRepository>();
    public IStopTimeRepository StopTimeRepository { get; } = Substitute.For<IStopTimeRepository>();
    public JourneyManager Manager { get; }

    // Lookup ve mutation bagimliliklari kullanilmaz; yalniz kapsamda durak yukleme siniri ayrilir.
    public JourneyPlanningFixture()
    {
        var stops = Substitute.ForPartsOf<StopManager>(StopRepository, (FeedManager)null!,
            Substitute.For<IRepository<LocationType, int>>(), Substitute.For<IRepository<WheelchairBoarding, int>>(),
            Substitute.For<IRepository<StopAccess, int>>(), (LevelManager)null!, ServiceRepository,
            StopTimeRepository, _pathwayRepository, _routeRepository, _transferRepository);
        stops.Configure().EnsureExistsInScopeAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(call => _stops.Single(stop => stop.Id == call.ArgAt<Guid>(0)));
        BindRepositoryReads();
        Manager = new JourneyManager(stops, StopRepository, ServiceRepository, StopTimeRepository,
            _tripRepository, _routeRepository, _transferRepository, _pathwayRepository);
    }

    // Farkli Feed'lerde ayni GTFS dis kimligi bulunabilir; ic kimliklerin farkli oldugunu korur.
    public Stop AddStop(Guid versionId, double? latitude = null, double? longitude = null)
    {
        var stop = new Stop(Guid.NewGuid(), versionId);
        Set(stop, nameof(Stop.GtfsStopId), "central");
        Set(stop, nameof(Stop.Name), "Central station");
        _stops.Add(stop);
        _places.Add(new StopPlaceModel { Id = stop.Id, LocationTypeCode = 0, Latitude = latitude, Longitude = longitude });
        return stop;
    }

    // Gercek sefer ve guzergah ayrintilarini servis gunune ait ziyaretlerle birlikte kurar.
    public Trip AddTrip(Guid versionId, DateOnly date, params (Stop Stop, string Time)[] calls)
    {
        var serviceId = Guid.NewGuid();
        if (!_dailyServices.TryGetValue(date, out var services))
            _dailyServices[date] = services = [];
        services.Add(serviceId);
        var route = new Route(Guid.NewGuid(), versionId);
        Set(route, nameof(Route.GtfsRouteId), "1");
        _routes.Add(route);
        var trip = new Trip(Guid.NewGuid(), versionId);
        Set(trip, nameof(Trip.RouteId), route.Id);
        Set(trip, nameof(Trip.ServiceId), serviceId);
        Set(trip, nameof(Trip.GtfsTripId), "morning");
        _trips.Add(trip);
        _visits[serviceId] = calls.Select((call, index) => new StopVisitModel
        {
            TripId = trip.Id, StopId = call.Stop.Id, StopSequence = index + 1,
            ArrivalTime = call.Time, DepartureTime = call.Time
        }).ToList();
        return trip;
    }

    // Gercek tek yonlu pathway adiminin sure ve tabela bilgisini hazirlar.
    public void AddPathway(Stop from, Stop to, int seconds, bool bidirectional)
    {
        var pathway = new Pathway(Guid.NewGuid(), from.FeedId, "stairs");
        Set(pathway, nameof(Pathway.FromStopId), from.Id);
        Set(pathway, nameof(Pathway.ToStopId), to.Id);
        Set(pathway, nameof(Pathway.TraversalTime), seconds);
        Set(pathway, nameof(Pathway.PathwayMode), 2);
        Set(pathway, nameof(Pathway.IsBidirectional), bidirectional ? 1 : 0);
        Set(pathway, nameof(Pathway.SignpostedAs), "Platform A");
        _pathways.Add(pathway);
    }

    // transfers.txt'deki yasak, cografik yakinliktan uretilen yurume yolunu da kapatir.
    public void ForbidTransfer(Stop from, Stop to)
    {
        var transfer = new Transfer(Guid.NewGuid(), from.FeedId);
        Set(transfer, nameof(Transfer.FromStopId), from.Id);
        Set(transfer, nameof(Transfer.ToStopId), to.Id);
        Set(transfer, nameof(Transfer.TransferType), 3);
        _transfers.Add(transfer);
    }

    // Planlamanin istek tarihi, baslangic saati ve aktarma sinirini belirtir.
    public Task<JourneyPlanModel> PlanAsync(Stop from, Stop to, string time = "08:00:00", int maxTransfers = 1)
        => Manager.PlanAsync(new JourneyQueryModel
        {
            FromStopId = from.Id, ToStopId = to.Id, Date = Date, Time = time, MaxTransfers = maxTransfers
        });

    // Gercek repository filtreleme seam'ini taklit eder; tarih ve saat penceresi kurallari veri tarafinda uygulanir.
    private void BindRepositoryReads()
    {
        StopRepository.GetPlacesAsync(null, Arg.Any<CancellationToken>()).Returns(_places);
        StopRepository.GetListAsync(Arg.Any<Expression<Func<Stop, bool>>>(), true, Arg.Any<CancellationToken>())
            .Returns(call => _stops.Where(call.ArgAt<Expression<Func<Stop, bool>>>(0).Compile()).ToList());
        _tripRepository.GetListAsync(Arg.Any<Expression<Func<Trip, bool>>>(), true, Arg.Any<CancellationToken>())
            .Returns(call => _trips.Where(call.ArgAt<Expression<Func<Trip, bool>>>(0).Compile()).ToList());
        _routeRepository.GetListAsync(Arg.Any<Expression<Func<Route, bool>>>(), true, Arg.Any<CancellationToken>())
            .Returns(call => _routes.Where(call.ArgAt<Expression<Func<Route, bool>>>(0).Compile()).ToList());
        _pathwayRepository.GetListAsync(false, Arg.Any<CancellationToken>()).Returns(_pathways);
        _transferRepository.GetListAsync(Arg.Any<Expression<Func<Transfer, bool>>>(), false, Arg.Any<CancellationToken>())
            .Returns(call => _transfers.Where(call.ArgAt<Expression<Func<Transfer, bool>>>(0).Compile()).ToList());
        ServiceRepository.GetActiveIdsAsync(null, Arg.Any<DateOnly>(), Arg.Any<CancellationToken>())
            .Returns(call => _dailyServices.GetValueOrDefault(call.ArgAt<DateOnly>(1), []));
        StopTimeRepository.GetVisitsAsync(null, Arg.Any<IReadOnlyCollection<Guid>>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<CancellationToken>())
            .Returns(call => VisitsInWindow(call.ArgAt<IReadOnlyCollection<Guid>>(1), call.ArgAt<string>(2), call.ArgAt<string>(3)));
    }

    // Secili servis gununun yalniz arama penceresinde varan/kalkan ziyaretlerini verir.
    private List<StopVisitModel> VisitsInWindow(IReadOnlyCollection<Guid> serviceIds, string fromTime, string toTime)
    {
        var from = fromTime.ToServiceSeconds();
        var to = toTime.ToServiceSeconds();
        return serviceIds.SelectMany(service => _visits.GetValueOrDefault(service, []))
            .Where(visit => visit.DepartureTime.ToServiceSeconds() >= from && visit.DepartureTime.ToServiceSeconds() <= to
                || visit.ArrivalTime.ToServiceSeconds() >= from && visit.ArrivalTime.ToServiceSeconds() <= to).ToList();
    }

    // Persistence alanlarini yalniz testte, uretim entity setter'larini acmadan doldurur.
    private static void Set<TEntity>(TEntity entity, string property, object value) where TEntity : class
        => typeof(TEntity).GetProperty(property)!.SetValue(entity, value);
}
