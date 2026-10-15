using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.CalendarDates;

// islevi: calendar_dates.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: CalendarDate ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.CalendarDates, GtfsImportOrders.CalendarDate)]
public class CalendarDateImportDto
{
    [Name(GtfsColumnNames.ServiceId), GtfsReference(ServiceConsts.TableName)]
    public Guid ServiceId { get; set; }

    [Name(GtfsColumnNames.ExceptionType), GtfsReference(ExceptionTypeConsts.TableName)]
    public int ExceptionTypeId { get; set; }

    [Name(GtfsColumnNames.Date)]
    public DateOnly Date { get; set; }
}
