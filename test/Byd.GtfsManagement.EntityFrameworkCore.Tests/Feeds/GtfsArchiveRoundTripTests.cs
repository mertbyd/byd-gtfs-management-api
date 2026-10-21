using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Byd.GtfsManagement.Consts;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Entities.FeedInfos;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Entities.Levels;
using Byd.GtfsManagement.Entities.Pathways;
using Byd.GtfsManagement.Entities.Stops;
using Byd.GtfsManagement.ExceptionCodes;
using Volo.Abp;
using Xunit;

namespace Byd.GtfsManagement.EntityFrameworkCore.Feeds;

// islevi: Uretimin ZIP/CSV import ve export yolunu surumlu GTFS referanslariyla sinar.
// sistemdeki gorevi: Sadece dosya adini dogrulamak yerine ozgun kimlik, Unicode metin, null ve baglantilarin korunmasini kanitlar.
public class GtfsArchiveRoundTripTests
{
    [Fact]
    public async Task FeedInfo_RoundTripPreservesPublisherAndOptionalValues()
    {
        using var fixture = new GtfsArchiveRoundTripFixture();
        var tenantId = Guid.NewGuid();
        var organizationId = Guid.NewGuid();
        var original = new Feed(Guid.NewGuid(), tenantId, organizationId, "Original", FeedSourceType.Upload, null);
        var copied = new Feed(Guid.NewGuid(), tenantId, organizationId, "Copied", FeedSourceType.Upload, null);
        var archive = GtfsArchiveRoundTripFixture.CreateArchive((GtfsFileNames.FeedInfo,
            "feed_publisher_name,feed_publisher_url,feed_lang,feed_version,feed_contact_email\n" +
            "Transit Office,https://example.org,tr,2026-10,info@example.org\n"));

        await fixture.ImportAsync(original, archive);
        var exported = await fixture.ExportAsync(original);
        await fixture.ImportAsync(copied, exported);

        var info = Assert.Single(fixture.Records<FeedInfo>(copied.Id));
        Assert.Equal("Transit Office", info.PublisherName);
        Assert.Equal("https://example.org", info.PublisherUrl);
        Assert.Equal("tr", info.Language);
        Assert.Equal("2026-10", info.Version);
        Assert.Equal("info@example.org", info.ContactEmail);
        Assert.Null(info.StartDate);
    }

    // ZIP export sonrasi yeni surume import ozgun GTFS degerlerini korur, ic kimlikleri yeni surume gore yeniler.
    [Fact]
    public async Task ImportExportImport_PreservesKeysUnicodeOptionalValuesAndVersionLocalRelations()
    {
        using var fixture = new GtfsArchiveRoundTripFixture();
        var tenantId = Guid.NewGuid();
        var feedId = Guid.NewGuid();
        var original = new Feed(Guid.NewGuid(), tenantId, feedId, "Uploaded ZIP", FeedSourceType.Upload, null);
        var copied = new Feed(Guid.NewGuid(), tenantId, feedId, "Exported ZIP", FeedSourceType.Upload, null);
        await fixture.ImportAsync(original, BuildStationArchive());

        var exported = await fixture.ExportAsync(original);
        using (var zip = new ZipArchive(new MemoryStream(exported), ZipArchiveMode.Read))
        {
            Assert.Equal(new[] { GtfsFileNames.Levels, GtfsFileNames.Pathways, GtfsFileNames.Stops }.Order(),
                zip.Entries.Select(entry => entry.FullName).Order());
            using var reader = new StreamReader(zip.GetEntry(GtfsFileNames.Stops)!.Open());
            var header = (await reader.ReadLineAsync())!.Split(',');
            Assert.Equal(1, header.Count(column => column == GtfsColumnNames.StopId));
        }
        await fixture.ImportAsync(copied, exported);

        AssertStationRows(fixture, original);
        AssertStationRows(fixture, copied);
        Assert.Empty(fixture.Records<Stop>(original.Id).Select(stop => stop.Id)
            .Intersect(fixture.Records<Stop>(copied.Id).Select(stop => stop.Id)));
    }

    // Ayni ZIP iki ayri surume alindiginda ayni dis anahtarlar farkli ic kimliklere doner.
    [Fact]
    public async Task RepeatedImportIntoDifferentVersions_DoesNotShareInternalKeys()
    {
        using var fixture = new GtfsArchiveRoundTripFixture();
        var tenantId = Guid.NewGuid();
        var feedId = Guid.NewGuid();
        var first = new Feed(Guid.NewGuid(), tenantId, feedId, "First", FeedSourceType.Upload, null);
        var second = new Feed(Guid.NewGuid(), tenantId, feedId, "Second", FeedSourceType.Upload, null);
        var archive = BuildStationArchive();
        await fixture.ImportAsync(first, archive);
        await fixture.ImportAsync(second, archive);

        AssertStationRows(fixture, first);
        AssertStationRows(fixture, second);
        Assert.NotEqual(fixture.Records<Level>(first.Id)[0].Id, fixture.Records<Level>(second.Id)[0].Id);
        Assert.Empty(fixture.Records<Stop>(first.Id).Select(stop => stop.Id)
            .Intersect(fixture.Records<Stop>(second.Id).Select(stop => stop.Id)));
    }

    // Tanimsiz lookup kodu dosya/satir/alan bilgili is hatasi olarak korunur.
    [Fact]
    public async Task UnknownLookupCode_ReturnsStructuredCsvFailure()
    {
        using var fixture = new GtfsArchiveRoundTripFixture();
        var version = new Feed(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Invalid code", FeedSourceType.Upload, null);
        var archive = GtfsArchiveRoundTripFixture.CreateArchive((GtfsFileNames.Stops,
            "stop_id,stop_name,location_type\nS1,Central,99\n"));

        var failure = await Assert.ThrowsAsync<BusinessException>(() => fixture.ImportAsync(version, archive));

        Assert.Equal(FeedExceptionCodes.LookupCodeUnknown, failure.Code);
        Assert.Equal(GtfsFileNames.Stops, failure.Data[FeedConsts.FileDataKey]);
        Assert.Equal(GtfsColumnNames.LocationType, failure.Data[FeedConsts.FieldDataKey]);
        Assert.Equal(2, failure.Data[FeedConsts.RowDataKey]);
    }

    // Cevirilemeyen koordinat kullanicinin duzeltebilecegi hucreyi tam olarak bildirir.
    [Fact]
    public async Task InvalidCoordinate_ReturnsFileRowAndColumn()
    {
        using var fixture = new GtfsArchiveRoundTripFixture();
        var version = new Feed(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "Invalid coordinate", FeedSourceType.Upload, null);
        var archive = GtfsArchiveRoundTripFixture.CreateArchive((GtfsFileNames.Stops,
            "stop_id,stop_name,stop_lat,stop_lon\nS1,Central,not-a-number,25.3\n"));

        var failure = await Assert.ThrowsAsync<BusinessException>(() => fixture.ImportAsync(version, archive));

        Assert.Equal(FeedExceptionCodes.RecordInvalid, failure.Code);
        Assert.Equal(GtfsFileNames.Stops, failure.Data[FeedConsts.FileDataKey]);
        Assert.Equal(GtfsColumnNames.StopLat, failure.Data[FeedConsts.FieldDataKey]);
        Assert.Equal(2, failure.Data[FeedConsts.RowDataKey]);
    }

    // Bir istasyon, iki kat ve katlar arasi yaya yolunu semantik GTFS degerleriyle kurar.
    private static byte[] BuildStationArchive() => GtfsArchiveRoundTripFixture.CreateArchive(
        (GtfsFileNames.Levels, "level_id,level_index,level_name\nL0,0,Street\nL-1,-1,Platform\n"),
        (GtfsFileNames.Stops,
            "stop_id,stop_name,stop_lat,stop_lon,location_type,parent_station,level_id,platform_code\n" +
            "S1,\"Vilnius, \"\"Žaliasis\"\" tiltas\",54.688,25.280,1,,L0,\n" +
            "S2,Platform A,54.6881,25.2801,0,S1,L-1,A\n"),
        (GtfsFileNames.Pathways,
            "pathway_id,from_stop_id,to_stop_id,pathway_mode,is_bidirectional,length,traversal_time,signposted_as\n" +
            "P1,S1,S2,2,1,42.5,90,\"Peronas, A\"\n"));

    // Ozgun anahtarlar ve yalniz ayni surumun ic kimliklerine bakan baglantilar korunmustur.
    private static void AssertStationRows(GtfsArchiveRoundTripFixture fixture, Feed version)
    {
        var stops = fixture.Records<Stop>(version.Id);
        var station = Assert.Single(stops, stop => stop.GtfsStopId == "S1");
        var platform = Assert.Single(stops, stop => stop.GtfsStopId == "S2");
        var levels = fixture.Records<Level>(version.Id);
        var ground = Assert.Single(levels, level => level.GtfsLevelId == "L0");
        var underground = Assert.Single(levels, level => level.GtfsLevelId == "L-1");
        var pathway = Assert.Single(fixture.Records<Pathway>(version.Id));
        Assert.Equal("Vilnius, \"Žaliasis\" tiltas", station.Name);
        Assert.Equal(54.688, station.Latitude);
        Assert.Null(station.ParentStationId);
        Assert.Null(station.PlatformCode);
        Assert.Equal(station.Id, platform.ParentStationId);
        Assert.Equal(ground.Id, station.LevelId);
        Assert.Equal(underground.Id, platform.LevelId);
        Assert.Equal("P1", pathway.GtfsPathwayId);
        Assert.Equal(station.Id, pathway.FromStopId);
        Assert.Equal(platform.Id, pathway.ToStopId);
        Assert.Equal(42.5, pathway.Length);
        Assert.Equal(90, pathway.TraversalTime);
        Assert.Equal("Peronas, A", pathway.SignpostedAs);
    }
}
