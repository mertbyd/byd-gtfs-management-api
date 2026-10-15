using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Networks;

// islevi: networks.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Network ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Networks, GtfsImportOrders.Network)]
public class NetworkImportDto
{
    [Name(GtfsColumnNames.NetworkId), GtfsReference(NetworkConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.NetworkId)]
    public string GtfsNetworkId { get; set; } = default!;

    [Name(GtfsColumnNames.NetworkName)]
    public string? NetworkName { get; set; }
}
