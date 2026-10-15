using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.FareAttributes;

// islevi: FareAttribute liste filtrelerini DataTable motoruna tanitir.
// sistemdeki gorevi: Ucret ozelligi sorgusunun izinli alanlarini belirler.
/// <summary>GTFS ucret ozelligi filtrelerini tasir.</summary>
public class FareAttributeFilter : IFeedFilter
{
    [DataTable(FareAttributeFilterFields.FeedId, FareAttributeFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(FareAttributeFilterFields.GtfsFareId, FareAttributeFilterFields.PropertyPaths.GtfsFareId)]
    public DataTableFilterItem? GtfsFareId { get; set; }

    [DataTable(FareAttributeFilterFields.AgencyId, FareAttributeFilterFields.PropertyPaths.AgencyId)]
    public DataTableFilterItem? AgencyId { get; set; }

    [DataTable(FareAttributeFilterFields.Price, FareAttributeFilterFields.PropertyPaths.Price)]
    public DataTableFilterItem? Price { get; set; }

    [DataTable(FareAttributeFilterFields.CurrencyType, FareAttributeFilterFields.PropertyPaths.CurrencyType)]
    public DataTableFilterItem? CurrencyType { get; set; }

    [DataTable(FareAttributeFilterFields.PaymentMethod, FareAttributeFilterFields.PropertyPaths.PaymentMethod)]
    public DataTableFilterItem? PaymentMethod { get; set; }

    [DataTable(FareAttributeFilterFields.Transfers, FareAttributeFilterFields.PropertyPaths.Transfers)]
    public DataTableFilterItem? Transfers { get; set; }

    [DataTable(FareAttributeFilterFields.TransferDuration, FareAttributeFilterFields.PropertyPaths.TransferDuration)]
    public DataTableFilterItem? TransferDuration { get; set; }

    [DataTable(FareAttributeFilterFields.CreationTime, FareAttributeFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
