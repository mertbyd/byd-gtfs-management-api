using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Frequencies;

// islevi: Frequency liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Siklik sorgusunun izinli alanlarini belirler.
/// <summary>Sefer sikligi filtrelerini tasir.</summary>
public class FrequencyFilter : IFeedFilter
{
    [DataTable(FrequencyFilterFields.FeedId, FrequencyFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FrequencyFilterFields.TripId, FrequencyFilterFields.PropertyPaths.TripId)]
    public DataTableFilterItem? TripId { get; set; }

    [DataTable(FrequencyFilterFields.StartTime, FrequencyFilterFields.PropertyPaths.StartTime)]
    public DataTableFilterItem? StartTime { get; set; }

    [DataTable(FrequencyFilterFields.EndTime, FrequencyFilterFields.PropertyPaths.EndTime)]
    public DataTableFilterItem? EndTime { get; set; }

    [DataTable(FrequencyFilterFields.HeadwaySecs, FrequencyFilterFields.PropertyPaths.HeadwaySecs)]
    public DataTableFilterItem? HeadwaySecs { get; set; }

    [DataTable(FrequencyFilterFields.CreationTime, FrequencyFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
