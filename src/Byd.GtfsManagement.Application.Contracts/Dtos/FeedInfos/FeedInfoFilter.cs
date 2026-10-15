using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FeedInfos;

// islevi: FeedInfo liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Yayimci sorgusunun izinli alanlarini belirler.
/// <summary>GTFS yayimci bilgisi filtrelerini tasir.</summary>
public class FeedInfoFilter : IFeedFilter
{
    [DataTable(FeedInfoFilterFields.FeedId, FeedInfoFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FeedInfoFilterFields.PublisherName, FeedInfoFilterFields.PropertyPaths.PublisherName)]
    public DataTableFilterItem? PublisherName { get; set; }

    [DataTable(FeedInfoFilterFields.Language, FeedInfoFilterFields.PropertyPaths.Language)]
    public DataTableFilterItem? Language { get; set; }

    [DataTable(FeedInfoFilterFields.DefaultLanguage, FeedInfoFilterFields.PropertyPaths.DefaultLanguage)]
    public DataTableFilterItem? DefaultLanguage { get; set; }

    [DataTable(FeedInfoFilterFields.StartDate, FeedInfoFilterFields.PropertyPaths.StartDate)]
    public DataTableFilterItem? StartDate { get; set; }

    [DataTable(FeedInfoFilterFields.EndDate, FeedInfoFilterFields.PropertyPaths.EndDate)]
    public DataTableFilterItem? EndDate { get; set; }

    [DataTable(FeedInfoFilterFields.Version, FeedInfoFilterFields.PropertyPaths.Version)]
    public DataTableFilterItem? Version { get; set; }

    [DataTable(FeedInfoFilterFields.CreationTime, FeedInfoFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
