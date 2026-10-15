using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FeedInfos;

// islevi: feed_info.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: FeedInfo ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.FeedInfo, GtfsImportOrders.FeedInfo)]
public class FeedInfoImportDto
{
    [Name(GtfsColumnNames.FeedPublisherName)]
    public string PublisherName { get; set; } = default!;

    [Name(GtfsColumnNames.FeedPublisherUrl)]
    public string PublisherUrl { get; set; } = default!;

    [Name(GtfsColumnNames.FeedLang)]
    public string Language { get; set; } = default!;

    [Name(GtfsColumnNames.DefaultLang)]
    public string? DefaultLanguage { get; set; }

    [Name(GtfsColumnNames.FeedStartDate)]
    public string? StartDate { get; set; }

    [Name(GtfsColumnNames.FeedEndDate)]
    public string? EndDate { get; set; }

    [Name(GtfsColumnNames.FeedVersion)]
    public string? Version { get; set; }

    [Name(GtfsColumnNames.FeedContactEmail)]
    public string? ContactEmail { get; set; }

    [Name(GtfsColumnNames.FeedContactUrl)]
    public string? ContactUrl { get; set; }
}
