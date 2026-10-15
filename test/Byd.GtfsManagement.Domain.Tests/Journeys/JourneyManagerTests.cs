using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Byd.GtfsManagement.Journeys;
using Xunit;

namespace Byd.GtfsManagement.Journeys;

// islevi: Bir organizasyonun birden fazla Feed'inin etkin surumlerinden gercek yolculuk kuruldugunu sinar.
// sistemdeki gorevi: Surum degisikligi planlayiciyi tek kaynaga kilitleyemez; gercek yurume/servis baglantisi olmadan rota uretilemez.
public class JourneyManagerTests
{
    // Otobus agindan rayli aga gecis yalniz gercek yakin duraklar arasindaki yurume bagiyla kurulur.
    [Fact]
    public async Task PlanAsync_CombinesTwoActiveVersionsThroughGenuineWalkingTransfer()
    {
        var fixture = new JourneyPlanningFixture();
        var firstVersion = Guid.NewGuid();
        var secondVersion = Guid.NewGuid();
        var origin = fixture.AddStop(firstVersion, 54.68, 25.28);
        var busTransfer = fixture.AddStop(firstVersion, 54.70, 25.30);
        var railTransfer = fixture.AddStop(secondVersion, 54.7001, 25.3001);
        var destination = fixture.AddStop(secondVersion, 54.72, 25.32);
        var bus = fixture.AddTrip(firstVersion, fixture.Date, (origin, "08:00:00"), (busTransfer, "08:10:00"));
        var rail = fixture.AddTrip(secondVersion, fixture.Date, (railTransfer, "08:12:00"), (destination, "08:20:00"));

        var result = await fixture.PlanAsync(origin, destination);

        var itinerary = Assert.Single(result.Itineraries);
        Assert.Equal("08:20:00", itinerary.ArrivalTime);
        Assert.Equal(1, itinerary.TransferCount);
        Assert.Equal(new[] { JourneyLegKind.Transit, JourneyLegKind.Walk, JourneyLegKind.Transit }, itinerary.Legs.Select(leg => leg.Kind));
        Assert.Equal(bus.Id, itinerary.Legs[0].TripId);
        Assert.Equal(rail.Id, itinerary.Legs[2].TripId);
        Assert.Equal(busTransfer.Id, itinerary.Legs[1].FromStopId);
        Assert.Equal(railTransfer.Id, itinerary.Legs[1].ToStopId);
        Assert.InRange(itinerary.Legs[1].DurationSeconds, 1, 30);
        Assert.Equal(secondVersion, itinerary.Legs[2].Route!.FeedId);
        await fixture.StopRepository.Received(1).GetPlacesAsync(null, Arg.Any<CancellationToken>());
        await fixture.ServiceRepository.Received(1).GetActiveIdsAsync(null, fixture.Date, Arg.Any<CancellationToken>());
    }

    // Uzak iki ag arasinda hizmet var diye gercekte olmayan yurume/aktarma cizgisi uretilmez.
    [Fact]
    public async Task PlanAsync_ReturnsNoItineraryWhenNetworksHaveNoPhysicalConnection()
    {
        var fixture = new JourneyPlanningFixture();
        var firstVersion = Guid.NewGuid();
        var secondVersion = Guid.NewGuid();
        var origin = fixture.AddStop(firstVersion, 54.68, 25.28);
        var busTransfer = fixture.AddStop(firstVersion, 54.70, 25.30);
        var railTransfer = fixture.AddStop(secondVersion, 54.71, 25.31);
        var destination = fixture.AddStop(secondVersion, 54.73, 25.33);
        fixture.AddTrip(firstVersion, fixture.Date, (origin, "08:00:00"), (busTransfer, "08:10:00"));
        fixture.AddTrip(secondVersion, fixture.Date, (railTransfer, "08:15:00"), (destination, "08:20:00"));

        var result = await fixture.PlanAsync(origin, destination);

        Assert.Empty(result.Itineraries);
        Assert.Same(origin, result.FromStop);
        Assert.Same(destination, result.ToStop);
    }

    // Yurume yolu bulunsa da ikinci araca zamaninda yetisilemezse yolculuk sunulmaz.
    [Fact]
    public async Task PlanAsync_DoesNotBoardAConnectingTripBeforeWalkFinishes()
    {
        var fixture = new JourneyPlanningFixture();
        var firstVersion = Guid.NewGuid();
        var secondVersion = Guid.NewGuid();
        var origin = fixture.AddStop(firstVersion, 54.68, 25.28);
        var busTransfer = fixture.AddStop(firstVersion, 54.70, 25.30);
        var railTransfer = fixture.AddStop(secondVersion, 54.7001, 25.3001);
        var destination = fixture.AddStop(secondVersion, 54.72, 25.32);
        fixture.AddTrip(firstVersion, fixture.Date, (origin, "08:00:00"), (busTransfer, "08:10:00"));
        fixture.AddTrip(secondVersion, fixture.Date, (railTransfer, "08:10:05"), (destination, "08:20:00"));

        Assert.Empty((await fixture.PlanAsync(origin, destination)).Itineraries);
    }

    // Aktarma istemeyen yolcuya iki aracli yolculuk teklif edilmez.
    [Fact]
    public async Task PlanAsync_RespectsZeroTransferLimitAcrossVersions()
    {
        var fixture = new JourneyPlanningFixture();
        var firstVersion = Guid.NewGuid();
        var secondVersion = Guid.NewGuid();
        var origin = fixture.AddStop(firstVersion, 54.68, 25.28);
        var busTransfer = fixture.AddStop(firstVersion, 54.70, 25.30);
        var railTransfer = fixture.AddStop(secondVersion, 54.7001, 25.3001);
        var destination = fixture.AddStop(secondVersion, 54.72, 25.32);
        fixture.AddTrip(firstVersion, fixture.Date, (origin, "08:00:00"), (busTransfer, "08:10:00"));
        fixture.AddTrip(secondVersion, fixture.Date, (railTransfer, "08:12:00"), (destination, "08:20:00"));

        Assert.Empty((await fixture.PlanAsync(origin, destination, maxTransfers: 0)).Itineraries);
    }

    // Onceki servis gununun 24:xx seferi yeni takvim gununde dogru saate kaydirilir ve daha gec bugun seferini gecer.
    [Fact]
    public async Task PlanAsync_IncludesPreviousServiceDaysAfterMidnightTrip()
    {
        var fixture = new JourneyPlanningFixture();
        var version = Guid.NewGuid();
        var origin = fixture.AddStop(version);
        var destination = fixture.AddStop(version);
        var overnight = fixture.AddTrip(version, fixture.Date.AddDays(-1), (origin, "24:05:00"), (destination, "24:20:00"));
        fixture.AddTrip(version, fixture.Date, (origin, "00:10:00"), (destination, "00:30:00"));

        var result = await fixture.PlanAsync(origin, destination, "00:00:00", 0);

        var itinerary = Assert.Single(result.Itineraries);
        Assert.Equal("00:05:00", itinerary.DepartureTime);
        Assert.Equal("00:20:00", itinerary.ArrivalTime);
        Assert.Equal(900, itinerary.DurationSeconds);
        Assert.Equal(overnight.Id, Assert.Single(itinerary.Legs).TripId);
        await fixture.ServiceRepository.Received(1).GetActiveIdsAsync(null, fixture.Date.AddDays(-1), Arg.Any<CancellationToken>());
    }

    // Yarin calisan bir sefer bugunun yolculuk secenegine sizamaz.
    [Fact]
    public async Task PlanAsync_DoesNotUseTripsFromAnotherServiceDate()
    {
        var fixture = new JourneyPlanningFixture();
        var version = Guid.NewGuid();
        var origin = fixture.AddStop(version);
        var destination = fixture.AddStop(version);
        fixture.AddTrip(version, fixture.Date.AddDays(1), (origin, "08:00:00"), (destination, "08:20:00"));

        Assert.Empty((await fixture.PlanAsync(origin, destination)).Itineraries);
    }

    // Ayni aracla ardisik duraklardan gecmek tek sefer adimidir; her durakta yeni binis sayilmaz.
    [Fact]
    public async Task PlanAsync_CollapsesConsecutiveVisitsOnTheSameTripIntoOneRide()
    {
        var fixture = new JourneyPlanningFixture();
        var version = Guid.NewGuid();
        var origin = fixture.AddStop(version);
        var middle = fixture.AddStop(version);
        var destination = fixture.AddStop(version);
        var trip = fixture.AddTrip(version, fixture.Date, (origin, "08:00:00"), (middle, "08:10:00"), (destination, "08:20:00"));

        var itinerary = Assert.Single((await fixture.PlanAsync(origin, destination, maxTransfers: 0)).Itineraries);

        Assert.Equal(0, itinerary.TransferCount);
        Assert.Equal(trip.Id, Assert.Single(itinerary.Legs).TripId);
        Assert.Equal(origin.Id, itinerary.Legs[0].FromStopId);
        Assert.Equal(destination.Id, itinerary.Legs[0].ToStopId);
    }

    // Tanimsiz koordinatlar durumunda gercek pathway sure ve yonuyle kullanilir.
    [Fact]
    public async Task PlanAsync_UsesOneWayPathwayWithoutInventingReverseAccess()
    {
        var fixture = new JourneyPlanningFixture();
        var version = Guid.NewGuid();
        var entrance = fixture.AddStop(version);
        var platform = fixture.AddStop(version);
        fixture.AddPathway(entrance, platform, 90, bidirectional: false);

        var itinerary = Assert.Single((await fixture.PlanAsync(entrance, platform)).Itineraries);
        var leg = Assert.Single(itinerary.Legs);

        Assert.Equal(JourneyLegKind.Pathway, leg.Kind);
        Assert.Equal(90, leg.DurationSeconds);
        Assert.Equal(2, leg.PathwayMode);
        Assert.Equal("Platform A", leg.SignpostedAs);
        Assert.Empty((await fixture.PlanAsync(platform, entrance)).Itineraries);
    }

    // transfers.txt yasagi birbirine cok yakin duraklar icin bile otomatik yurume yolunu kapatir.
    [Fact]
    public async Task PlanAsync_HonorsExplicitTransferProhibition()
    {
        var fixture = new JourneyPlanningFixture();
        var version = Guid.NewGuid();
        var from = fixture.AddStop(version, 54.70, 25.30);
        var to = fixture.AddStop(version, 54.7001, 25.3001);
        fixture.ForbidTransfer(from, to);

        Assert.Empty((await fixture.PlanAsync(from, to)).Itineraries);
    }
}
