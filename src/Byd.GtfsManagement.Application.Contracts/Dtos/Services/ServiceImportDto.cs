using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Services;

// islevi: calendar.txt ve calendar_dates.txt satirlarindaki service_id degerini tasir.
// sistemdeki gorevi: GTFS'te ayri bir servis dosyasi yoktur; servis kimligi iki takvim dosyasindan birlestirilerek
// Service kaydina eslenir. Uye adlari entity ile ayni oldugu icin Mapperly ad eslesmesiyle esler.
[GtfsFile([GtfsFileNames.Calendar, GtfsFileNames.CalendarDates], GtfsImportOrders.Service)]
public class ServiceImportDto
{
    [Name(GtfsColumnNames.ServiceId), GtfsReference(ServiceConsts.TableName)]
    public Guid Id { get; set; }

    [Name(GtfsColumnNames.ServiceId)]
    public string GtfsServiceId { get; set; } = default!;
}
