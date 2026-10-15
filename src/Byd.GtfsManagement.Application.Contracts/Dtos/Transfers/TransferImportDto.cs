using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Transfers;

// islevi: transfers.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Transfer ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Transfers, GtfsImportOrders.Transfer)]
public class TransferImportDto
{
    [Name(GtfsColumnNames.FromStopId), GtfsReference(StopConsts.TableName)]
    public Guid? FromStopId { get; set; }

    [Name(GtfsColumnNames.ToStopId), GtfsReference(StopConsts.TableName)]
    public Guid? ToStopId { get; set; }

    [Name(GtfsColumnNames.FromRouteId), GtfsReference(RouteConsts.TableName)]
    public Guid? FromRouteId { get; set; }

    [Name(GtfsColumnNames.ToRouteId), GtfsReference(RouteConsts.TableName)]
    public Guid? ToRouteId { get; set; }

    [Name(GtfsColumnNames.FromTripId), GtfsReference(TripConsts.TableName)]
    public Guid? FromTripId { get; set; }

    [Name(GtfsColumnNames.ToTripId), GtfsReference(TripConsts.TableName)]
    public Guid? ToTripId { get; set; }

    [Name(GtfsColumnNames.TransferType)]
    public int TransferType { get; set; }

    [Name(GtfsColumnNames.MinTransferTime)]
    public int? MinTransferTime { get; set; }
}
