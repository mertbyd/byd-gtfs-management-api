using Byd.GtfsManagement.Interfaces;
using Byd.CoreExtensions.Attributes;
using Byd.CoreExtensions.Querying;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Stops;

// islevi: Stop liste ucunun filtre alanlarini kararli tel adi ve property yoluyla tanitir.
// sistemdeki gorevi: DataTable motorunun kabul ettigi tipli filtre izin listesidir.
public class StopFilter : IFeedFilter
{
    [DataTable(StopFilterFields.FeedId, StopFilterFields.PropertyPaths.FeedId)]
    public DataTableFilterItem? FeedId { get; set; }

    [DataTable(StopFilterFields.LocationTypeId, StopFilterFields.PropertyPaths.LocationTypeId)]
    public DataTableFilterItem? LocationTypeId { get; set; }

    [DataTable(StopFilterFields.WheelchairBoardingId, StopFilterFields.PropertyPaths.WheelchairBoardingId)]
    public DataTableFilterItem? WheelchairBoardingId { get; set; }

    [DataTable(StopFilterFields.ParentStationId, StopFilterFields.PropertyPaths.ParentStationId)]
    public DataTableFilterItem? ParentStationId { get; set; }

    [DataTable(StopFilterFields.GtfsStopId, StopFilterFields.PropertyPaths.GtfsStopId)]
    public DataTableFilterItem? GtfsStopId { get; set; }

    [DataTable(StopFilterFields.Code, StopFilterFields.PropertyPaths.Code)]
    public DataTableFilterItem? Code { get; set; }

    [DataTable(StopFilterFields.Name, StopFilterFields.PropertyPaths.Name)]
    public DataTableFilterItem? Name { get; set; }

    [DataTable(StopFilterFields.ZoneId, StopFilterFields.PropertyPaths.ZoneId)]
    public DataTableFilterItem? ZoneId { get; set; }

    [DataTable(StopFilterFields.CreationTime, StopFilterFields.PropertyPaths.CreationTime)]
    public DataTableFilterItem? CreationTime { get; set; }
}
