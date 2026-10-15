using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Routes;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.Entities.Transfers;
using Byd.GtfsManagement.Entities.Trips;
using Byd.GtfsManagement.ExceptionCodes;
using Byd.GtfsManagement.Extensions.Gtfs;
using Byd.GtfsManagement.Interfaces.Pathways;
using Byd.GtfsManagement.Interfaces.Routes;
using Byd.GtfsManagement.Interfaces.Services;
using Byd.GtfsManagement.Interfaces.StopTimes;
using Byd.GtfsManagement.Interfaces.Stops;
using Byd.GtfsManagement.Interfaces.Transfers;
using Byd.GtfsManagement.Interfaces.Trips;
using Byd.GtfsManagement.Journeys;
using Byd.GtfsManagement.Managers.Stops;
using Byd.GtfsManagement.Models.Journeys;
using Byd.GtfsManagement.Models.StopTimes;
using Byd.GtfsManagement.Models.Stops;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Services;

namespace Byd.GtfsManagement.Managers.Journeys;

// islevi: Iki durak arasinda, verilen gun ve saatten sonra en erken varan yolculuk seceneklerini aktarma sayisina gore bulur.
// sistemdeki gorevi: Ardisik durak ziyaretlerinden baglanti; yakin duraklar, istasyon ici pathway ve transfers.txt kayitlarindan yurume
// yollari kurar. Baglantilari kalkis sirasiyla tarar ve her aktarma sayisi icin en erken varisi tutar. Veri okuma her tablonun kendi
// repository'sindedir; burada yalniz planlama kurali vardir.
public class JourneyManager(
    StopManager stopManager,
    IStopRepository stopRepository,
    IServiceRepository serviceRepository,
    IStopTimeRepository stopTimeRepository,
    ITripRepository tripRepository,
    IRouteRepository routeRepository,
    ITransferRepository transferRepository,
    IPathwayRepository pathwayRepository) : DomainService
{
    private const double CellLatitudeDegrees = JourneyConsts.MaxWalkMeters / 111_000;
    private const double CellLongitudeDegrees = CellLatitudeDegrees * 2;
    private const double EarthRadiusMeters = 6_371_000;

    // Duraklari kapsamda dogrular, agi kurar, tarar ve secenekleri durak, sefer ve guzergah ayrintilariyla dondurur.
    public virtual async Task<JourneyPlanModel> PlanAsync(JourneyQueryModel query, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query);
        var from = await stopManager.EnsureExistsInScopeAsync(query.FromStopId, cancellationToken);
        var to = await stopManager.EnsureExistsInScopeAsync(query.ToStopId, cancellationToken);
        var places = (await stopRepository.GetPlacesAsync(null, cancellationToken)).ToDictionary(place => place.Id);
        var departure = query.Time.ToServiceSeconds();
        var connections = await LoadConnectionsAsync(query.Date, departure, cancellationToken);
        var scan = await CreateScanAsync(places, query.MaxTransfers, cancellationToken);
        var itineraries = Scan(scan, connections, places, from.Id, to.Id, departure);
        return await DescribeAsync(from.Id, to.Id, itineraries, cancellationToken);
    }

    // Gunun ve gece yarisini asan onceki gun seferlerinin arama penceresindeki baglantilarini kalkis sirasiyla kurar.
    private async Task<List<JourneyConnectionModel>> LoadConnectionsAsync(
        DateOnly date, int departure, CancellationToken cancellationToken = default)
    {
        var today = await LoadDayConnectionsAsync(date, departure, 0, cancellationToken);
        var overnight = await LoadDayConnectionsAsync(date.AddDays(-1), departure + JourneyConsts.SecondsPerDay,
            -JourneyConsts.SecondsPerDay, cancellationToken);
        return today.Concat(overnight).OrderBy(connection => connection.DepartureSeconds).ToList();
    }

    // Bir servis gununun penceredeki ziyaretlerini okur ve ayni seferin ardisik ziyaretlerinden baglanti kurar.
    private async Task<List<JourneyConnectionModel>> LoadDayConnectionsAsync(
        DateOnly serviceDate, int windowStart, int shift, CancellationToken cancellationToken = default)
    {
        var serviceIds = await serviceRepository.GetActiveIdsAsync(null, serviceDate, cancellationToken);
        if (serviceIds.Count == 0)
            return [];
        var visits = await stopTimeRepository.GetVisitsAsync(null, serviceIds, windowStart.ToGtfsTime(),
            (windowStart + JourneyConsts.SearchWindowSeconds).ToGtfsTime(), cancellationToken);
        return visits.GroupBy(visit => visit.TripId)
            .SelectMany(trip => ToConnections(trip.OrderBy(visit => visit.StopSequence).ToList(), shift))
            .ToList();
    }

    // Ayni seferin ardisik iki ziyaretinden kalkis ve varis saniyeli baglanti uretir.
    private static IEnumerable<JourneyConnectionModel> ToConnections(List<StopVisitModel> visits, int shift)
        => visits.Zip(visits.Skip(1), (from, to) => new JourneyConnectionModel
        {
            TripId = from.TripId,
            FromStopId = from.StopId,
            ToStopId = to.StopId,
            DepartureSeconds = from.DepartureTime.ToServiceSeconds() + shift,
            ArrivalSeconds = to.ArrivalTime.ToServiceSeconds() + shift
        });

    // Tarama durumunu aktarma sinirina gore kurar ve yurume yollarini yukler.
    private async Task<JourneyScanModel> CreateScanAsync(
        Dictionary<Guid, StopPlaceModel> places, int maxTransfers, CancellationToken cancellationToken = default)
    {
        var rounds = maxTransfers + 2;
        return new JourneyScanModel
        {
            Labels = Enumerable.Range(0, rounds).Select(_ => new Dictionary<Guid, JourneyLabelModel>()).ToArray(),
            Ready = Enumerable.Range(0, rounds).Select(_ => new Dictionary<Guid, int>()).ToArray(),
            Edges = await LoadEdgesAsync(places, cancellationToken)
        };
    }

    // Yakin duraklar arasi yurume, istasyon ici gecis, pathway ve transfers.txt yollarini kurar.
    private async Task<Dictionary<Guid, List<JourneyFootpathModel>>> LoadEdgesAsync(
        Dictionary<Guid, StopPlaceModel> places, CancellationToken cancellationToken = default)
    {
        var pathways = await pathwayRepository.GetListAsync(includeDetails: false, cancellationToken: cancellationToken);
        var transfers = await transferRepository.GetListAsync(transfer => transfer.FromStopId != null && transfer.ToStopId != null && transfer.FromTripId == null && transfer.ToTripId == null
            && transfer.FromRouteId == null && transfer.ToRouteId == null, false, cancellationToken);
        var edges = new Dictionary<(Guid From, Guid To), JourneyFootpathModel>();
        AddWalkingEdges(edges, places.Values);
        AddStationEdges(edges, places.Values, pathways, places);
        AddPathwayEdges(edges, pathways);
        ApplyTransfers(edges, transfers);
        return edges.GroupBy(edge => edge.Key.From).ToDictionary(group => group.Key, group => group.Select(edge => edge.Value).ToList());
    }

    // Duraklari kucuk hucrelere dagitip komsu hucrelerdeki yakin duraklar arasina yurume yolu ekler.
    private static void AddWalkingEdges(Dictionary<(Guid, Guid), JourneyFootpathModel> edges, IEnumerable<StopPlaceModel> places)
    {
        var located = places.Where(place => place is { Latitude: not null, Longitude: not null }
            && place.LocationTypeCode == LocationTypeConsts.StopOrPlatformCode).ToList();
        var cells = located.ToLookup(Cell);
        foreach (var place in located)
            foreach (var neighbour in Neighbours(cells, Cell(place)).Where(neighbour => neighbour.Id != place.Id))
                AddWalk(edges, place, neighbour);
    }

    // Iki durak yurume mesafesindeyse aralarina yurume suresiyle yol ekler.
    private static void AddWalk(Dictionary<(Guid, Guid), JourneyFootpathModel> edges, StopPlaceModel from, StopPlaceModel to)
    {
        var meters = Distance(from, to);
        if (meters <= JourneyConsts.MaxWalkMeters)
            edges.TryAdd((from.Id, to.Id), Footpath(to.Id, (int)Math.Ceiling(meters / JourneyConsts.WalkMetersPerSecond),
                JourneyLegKind.Walk));
    }

    // pathways.txt tanimlamayan istasyonlarin peronlari arasina sabit istasyon ici aktarma suresi ekler.
    private static void AddStationEdges(Dictionary<(Guid, Guid), JourneyFootpathModel> edges, IEnumerable<StopPlaceModel> places,
        List<Pathway> pathways, Dictionary<Guid, StopPlaceModel> byId)
    {
        var withPathways = pathways.Select(pathway => byId.GetValueOrDefault(pathway.FromStopId)?.ParentStationId)
            .Where(station => station is not null).ToHashSet();
        foreach (var station in places.Where(place => place.ParentStationId is not null && !withPathways.Contains(place.ParentStationId))
                     .GroupBy(place => place.ParentStationId))
            foreach (var (from, to) in station.SelectMany(from => station.Where(to => to.Id != from.Id), (from, to) => (from, to)))
                edges.TryAdd((from.Id, to.Id), Footpath(to.Id, JourneyConsts.StationTransferSeconds, JourneyLegKind.Transfer));
    }

    // pathways.txt yollarini gecis suresi, yoksa uzunluk ve yurume hiziyla ekler; cift yonlu yolu ters yonde de ekler.
    private static void AddPathwayEdges(Dictionary<(Guid, Guid), JourneyFootpathModel> edges, List<Pathway> pathways)
    {
        foreach (var pathway in pathways)
        {
            var seconds = pathway.TraversalTime
                ?? (pathway.Length is { } length ? (int)Math.Ceiling(length / JourneyConsts.WalkMetersPerSecond)
                    : JourneyConsts.StationTransferSeconds);
            edges[(pathway.FromStopId, pathway.ToStopId)] = Footpath(pathway.ToStopId, seconds, JourneyLegKind.Pathway,
                pathway.PathwayMode, pathway.SignpostedAs);
            if (pathway.IsBidirectional == PathwayConsts.Bidirectional)
                edges[(pathway.ToStopId, pathway.FromStopId)] = Footpath(pathway.FromStopId, seconds, JourneyLegKind.Pathway,
                    pathway.PathwayMode, pathway.ReversedSignpostedAs ?? pathway.SignpostedAs);
        }
    }

    // transfers.txt kayitlarini uygular: aktarmanin mumkun olmadigi ciftleri cikarir, digerlerine asgari sureyi yazar.
    private static void ApplyTransfers(Dictionary<(Guid, Guid), JourneyFootpathModel> edges, List<Transfer> transfers)
    {
        foreach (var transfer in transfers)
        {
            var key = (transfer.FromStopId!.Value, transfer.ToStopId!.Value);
            if (transfer.TransferType == TransferConsts.NotPossibleCode)
                edges.Remove(key);
            else
                edges[key] = Footpath(key.Item2, transfer.MinTransferTime
                    ?? edges.GetValueOrDefault(key)?.Seconds ?? JourneyConsts.MinChangeSeconds, JourneyLegKind.Transfer);
        }
    }

    // Baglantilari kalkis sirasiyla tarar ve her aktarma sayisi icin hedefe en erken varan secenegi geri kurar.
    private static List<JourneyItineraryModel> Scan(JourneyScanModel scan, List<JourneyConnectionModel> connections,
        Dictionary<Guid, StopPlaceModel> places, Guid fromId, Guid toId, int departure)
    {
        foreach (var origin in WithChildren(places, fromId))
            Reach(scan, 0, origin, new JourneyLabelModel { ArrivalSeconds = departure, DepartureSeconds = departure }, departure);
        foreach (var connection in connections.Where(connection => connection.DepartureSeconds >= departure))
            Ride(scan, connection);
        return Collect(scan, WithChildren(places, toId).ToList());
    }

    // Baglantinin seferine binilebiliyorsa varis duragini etiketler ve oradan yurume yollarini yayar.
    private static void Ride(JourneyScanModel scan, JourneyConnectionModel connection)
    {
        if (Board(scan, connection) is not { } boarding)
            return;
        var label = new JourneyLabelModel
        {
            ArrivalSeconds = connection.ArrivalSeconds, DepartureSeconds = boarding.DepartureSeconds,
            PreviousStopId = boarding.BoardStopId, TripId = connection.TripId
        };
        Reach(scan, boarding.Round, connection.ToStopId, label, connection.ArrivalSeconds + ChangeSeconds(scan, connection.ToStopId));
    }

    // Seferin bilinen binis noktasini dondurur; bu baglantinin duragindan daha az seferle binilebiliyorsa binis noktasini gunceller.
    private static JourneyBoardingModel? Board(JourneyScanModel scan, JourneyConnectionModel connection)
    {
        var current = scan.Boardings.GetValueOrDefault(connection.TripId);
        var candidate = Enumerable.Range(0, scan.Ready.Length - 1).FirstOrDefault(round =>
            scan.Ready[round].TryGetValue(connection.FromStopId, out var ready) && ready <= connection.DepartureSeconds, -1);
        if (candidate < 0 || current is not null && candidate + 1 >= current.Round)
            return current;
        return scan.Boardings[connection.TripId] = new JourneyBoardingModel
        {
            Round = candidate + 1, BoardStopId = connection.FromStopId, DepartureSeconds = connection.DepartureSeconds
        };
    }

    // Duraga daha erken varildiysa etiketi yazar, binise hazir olma saatini gunceller ve yurume yollarini yayar.
    private static void Reach(JourneyScanModel scan, int round, Guid stopId, JourneyLabelModel label, int readySeconds)
    {
        if (!Improves(scan, round, stopId, label.ArrivalSeconds))
            return;
        scan.Labels[round][stopId] = label;
        MarkReady(scan, round, stopId, readySeconds);
        foreach (var walk in ReachableFrom(scan, stopId))
            Walk(scan, round, stopId, label.ArrivalSeconds, walk);
    }

    // Yurume sonundaki duraga daha erken varildiysa gecilen yollarla yurume etiketini yazar.
    private static void Walk(JourneyScanModel scan, int round, Guid fromStopId, int departure, JourneyWalkModel walk)
    {
        var arrival = departure + walk.Seconds;
        if (!Improves(scan, round, walk.TargetStopId, arrival))
            return;
        scan.Labels[round][walk.TargetStopId] = new JourneyLabelModel
        {
            ArrivalSeconds = arrival, DepartureSeconds = departure, PreviousStopId = fromStopId, Hops = walk.Hops
        };
        MarkReady(scan, round, walk.TargetStopId, arrival);
    }

    // Duraga bu aktarma sayisiyla daha once daha erken varilmadiysa dogrudur.
    private static bool Improves(JourneyScanModel scan, int round, Guid stopId, int arrival)
        => !scan.Labels[round].TryGetValue(stopId, out var existing) || arrival < existing.ArrivalSeconds;

    // Duraktan bu aktarma sayisiyla binise hazir olunan en erken saati tutar.
    private static void MarkReady(JourneyScanModel scan, int round, Guid stopId, int ready)
    {
        if (!scan.Ready[round].TryGetValue(stopId, out var existing) || ready < existing)
            scan.Ready[round][stopId] = ready;
    }

    // Ayni durakta arac degistirme suresi; transfers.txt ayni durak icin sure veriyorsa o kullanilir.
    private static int ChangeSeconds(JourneyScanModel scan, Guid stopId)
        => scan.Edges.GetValueOrDefault(stopId)?.FirstOrDefault(edge => edge.TargetStopId == stopId)?.Seconds
            ?? JourneyConsts.MinChangeSeconds;

    // Duraktan sure sinirinda erisilen duraklari en kisa sureleri ve gecilen yollariyla bulur; sonuc istek boyunca saklanir.
    private static List<JourneyWalkModel> ReachableFrom(JourneyScanModel scan, Guid origin)
    {
        if (scan.Reachable.TryGetValue(origin, out var known))
            return known;
        var best = new Dictionary<Guid, JourneyWalkModel> { [origin] = new() { TargetStopId = origin } };
        var queue = new PriorityQueue<Guid, int>();
        queue.Enqueue(origin, 0);
        while (queue.TryDequeue(out var stopId, out var seconds))
            foreach (var edge in Outgoing(scan, stopId, seconds, best))
                Relax(best, queue, best[stopId], edge);
        best.Remove(origin);
        return scan.Reachable[origin] = best.Values.ToList();
    }

    // Duragin guncel en kisa suresi hala gecerliyse kendisine donmeyen cikis yollarini verir.
    private static IEnumerable<JourneyFootpathModel> Outgoing(JourneyScanModel scan, Guid stopId, int seconds,
        Dictionary<Guid, JourneyWalkModel> best)
        => seconds > best[stopId].Seconds
            ? []
            : scan.Edges.GetValueOrDefault(stopId, []).Where(edge => edge.TargetStopId != stopId);

    // Yol sure siniri icinde daha kisa bir varis veriyorsa onu, oraya kadar gecilen yollara ekleyerek kaydeder.
    private static void Relax(Dictionary<Guid, JourneyWalkModel> best, PriorityQueue<Guid, int> queue,
        JourneyWalkModel current, JourneyFootpathModel edge)
    {
        var seconds = current.Seconds + edge.Seconds;
        if (seconds > JourneyConsts.MaxFootpathSeconds
            || best.TryGetValue(edge.TargetStopId, out var known) && known.Seconds <= seconds)
            return;
        best[edge.TargetStopId] = new JourneyWalkModel
        {
            TargetStopId = edge.TargetStopId, Seconds = seconds, Hops = [.. current.Hops, edge]
        };
        queue.Enqueue(edge.TargetStopId, seconds);
    }

    // Her aktarma sayisi icin hedefe en erken varisi bulur; daha az aktarmayla daha erken varan secenegi gecemeyeni eler.
    private static List<JourneyItineraryModel> Collect(JourneyScanModel scan, List<Guid> targets)
    {
        var itineraries = new List<JourneyItineraryModel>();
        var earliest = int.MaxValue;
        for (var round = 0; round < scan.Labels.Length; round++)
        {
            var labels = scan.Labels[round];
            var reached = targets.Where(target => labels.TryGetValue(target, out var label) && label.PreviousStopId is not null).ToList();
            if (reached.Count == 0)
                continue;
            var best = reached.MinBy(target => labels[target].ArrivalSeconds);
            if (labels[best].ArrivalSeconds >= earliest)
                continue;
            earliest = labels[best].ArrivalSeconds;
            itineraries.Add(Rebuild(scan, round, best));
        }
        return itineraries;
    }

    // Etiketlerin onceki duraklarini hedeften baslangica izleyerek secenegin adimlarini kurar.
    private static JourneyItineraryModel Rebuild(JourneyScanModel scan, int round, Guid stopId)
    {
        var legs = new List<JourneyLegModel>();
        while (scan.Labels[round][stopId] is { PreviousStopId: { } previous } label)
        {
            legs.InsertRange(0, Legs(label, previous, stopId));
            round -= label.TripId is null ? 0 : 1;
            stopId = previous;
        }
        return new JourneyItineraryModel
        {
            DepartureTime = legs[0].DepartureTime,
            ArrivalTime = legs[^1].ArrivalTime,
            DurationSeconds = legs[^1].ArrivalTime.ToServiceSeconds() - legs[0].DepartureTime.ToServiceSeconds(),
            TransferCount = Math.Max(0, legs.Count(leg => leg.Kind == JourneyLegKind.Transit) - 1),
            Legs = legs
        };
    }

    // Etiketin adimlarini kurar: sefer etiketi tek adimdir ve sefer bilgisi sonradan tamamlanir; yurume etiketi gectigi her yol
    // icin, yolun turu ve tabela metniyle ayri adim verir.
    private static IEnumerable<JourneyLegModel> Legs(JourneyLabelModel label, Guid fromId, Guid toId)
    {
        if (label.TripId is { } tripId)
            return [Leg(JourneyLegKind.Transit, fromId, toId, label.DepartureSeconds, label.ArrivalSeconds, tripId)];
        var legs = new List<JourneyLegModel>();
        var departure = label.DepartureSeconds;
        foreach (var hop in label.Hops)
        {
            legs.Add(Hop(hop, fromId, departure));
            (fromId, departure) = (hop.TargetStopId, departure + hop.Seconds);
        }
        return legs;
    }

    // Yurume, pathway veya aktarma yolundan turu, yol bicimi ve tabela metniyle tek adim kurar.
    private static JourneyLegModel Hop(JourneyFootpathModel hop, Guid fromId, int departure)
    {
        var leg = Leg(hop.Kind, fromId, hop.TargetStopId, departure, departure + hop.Seconds);
        leg.PathwayMode = hop.PathwayMode;
        leg.SignpostedAs = hop.SignpostedAs;
        return leg;
    }

    // Iki durak ve iki an arasindaki tek adimi kimliklerle kurar; sefer adiminda sefer kimligi tasinir.
    private static JourneyLegModel Leg(JourneyLegKind kind, Guid fromId, Guid toId, int departure, int arrival, Guid? tripId = null)
        => new()
        {
            Kind = kind,
            FromStopId = fromId,
            ToStopId = toId,
            TripId = tripId,
            DepartureTime = departure.ToGtfsTime(),
            ArrivalTime = arrival.ToGtfsTime(),
            DurationSeconds = arrival - departure
        };

    // Adimlarin duraklarini, seferlerini ve guzergahlarini cevapta gosterilecek ayrintilariyla tabloda birer sorguda okur.
    private async Task<JourneyPlanModel> DescribeAsync(Guid fromId, Guid toId, List<JourneyItineraryModel> itineraries,
        CancellationToken cancellationToken = default)
    {
        var legs = itineraries.SelectMany(itinerary => itinerary.Legs).ToList();
        var stops = await LoadAsync(stopRepository, legs.SelectMany(leg => new[] { leg.FromStopId, leg.ToStopId })
            .Append(fromId).Append(toId), cancellationToken);
        var trips = await LoadAsync(tripRepository, legs.Where(leg => leg.TripId is not null).Select(leg => leg.TripId!.Value),
            cancellationToken);
        var routes = await LoadAsync(routeRepository, trips.Values.Select(trip => trip.RouteId), cancellationToken);
        foreach (var leg in legs)
            Describe(leg, stops, trips, routes);
        return new JourneyPlanModel { FromStop = stops[fromId], ToStop = stops[toId], Itineraries = itineraries };
    }

    // Adimin kimliklerini okunan durak, sefer ve guzergah kayitlarina baglar.
    private static void Describe(JourneyLegModel leg, Dictionary<Guid, Stop> stops, Dictionary<Guid, Trip> trips,
        Dictionary<Guid, Route> routes)
    {
        leg.FromStop = stops[leg.FromStopId];
        leg.ToStop = stops[leg.ToStopId];
        leg.Trip = leg.TripId is { } tripId ? trips[tripId] : null;
        leg.Route = leg.Trip is { } trip ? routes[trip.RouteId] : null;
    }

    // Kimlikleri verilen kayitlari ayrintilariyla tek sorguda okuyup kimlige gore dizinler.
    private static async Task<Dictionary<Guid, TEntity>> LoadAsync<TEntity>(IExtendedRepository<TEntity> repository,
        IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        where TEntity : class, IEntity<Guid>
    {
        var keys = ids.Distinct().ToList();
        return (await repository.GetListAsync(entity => keys.Contains(entity.Id), true, cancellationToken))
            .ToDictionary(entity => entity.Id);
    }

    // Duragi ve istasyonsa ona bagli peron ve girisleri verir.
    private static IEnumerable<Guid> WithChildren(Dictionary<Guid, StopPlaceModel> places, Guid stopId)
        => places.Values.Where(place => place.ParentStationId == stopId).Select(place => place.Id).Append(stopId);

    // Tek yurume, pathway veya aktarma yolunu kurar.
    private static JourneyFootpathModel Footpath(Guid target, int seconds, JourneyLegKind kind, int? pathwayMode = null,
        string? signpostedAs = null)
        => new() { TargetStopId = target, Seconds = seconds, Kind = kind, PathwayMode = pathwayMode, SignpostedAs = signpostedAs };

    // Duragin bulundugu hucreyi verir.
    private static (int, int) Cell(StopPlaceModel place)
        => ((int)Math.Floor(place.Latitude!.Value / CellLatitudeDegrees), (int)Math.Floor(place.Longitude!.Value / CellLongitudeDegrees));

    // Hucre ve cevresindeki sekiz hucredeki duraklari verir.
    private static IEnumerable<StopPlaceModel> Neighbours(ILookup<(int, int), StopPlaceModel> cells, (int Row, int Column) cell)
        => from row in Enumerable.Range(cell.Row - 1, 3)
           from column in Enumerable.Range(cell.Column - 1, 3)
           from place in cells[(row, column)]
           select place;

    // Iki durak arasindaki buyuk daire mesafesini metre olarak verir.
    private static double Distance(StopPlaceModel from, StopPlaceModel to)
    {
        var latitude = ToRadians(to.Latitude!.Value - from.Latitude!.Value);
        var longitude = ToRadians(to.Longitude!.Value - from.Longitude!.Value);
        var a = Math.Pow(Math.Sin(latitude / 2), 2) + Math.Cos(ToRadians(from.Latitude.Value)) * Math.Cos(ToRadians(to.Latitude.Value))
            * Math.Pow(Math.Sin(longitude / 2), 2);
        return 2 * EarthRadiusMeters * Math.Asin(Math.Sqrt(a));
    }

    // Dereceyi radyana cevirir.
    private static double ToRadians(double degrees) => degrees * Math.PI / 180;
}
