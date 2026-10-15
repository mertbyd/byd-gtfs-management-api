using System;
using CsvHelper.Configuration.Attributes;
using Byd.GtfsManagement.Attributes;
using Byd.GtfsManagement.Consts;

namespace Byd.GtfsManagement.Dtos.Calendars;

// islevi: calendar.txt satirini GTFS kolon adlariyla tasir.
// sistemdeki gorevi: Calendar ice aktariminin CSV bicimidir; uye adlari entity ile ayni oldugu icin Mapperly ad
// eslesmesiyle esler.
[GtfsFile(GtfsFileNames.Calendar, GtfsImportOrders.Calendar)]
public class CalendarImportDto
{
    [Name(GtfsColumnNames.ServiceId), GtfsReference(ServiceConsts.TableName)]
    public Guid ServiceId { get; set; }

    [Name(GtfsColumnNames.Monday)]
    public bool Monday { get; set; }

    [Name(GtfsColumnNames.Tuesday)]
    public bool Tuesday { get; set; }

    [Name(GtfsColumnNames.Wednesday)]
    public bool Wednesday { get; set; }

    [Name(GtfsColumnNames.Thursday)]
    public bool Thursday { get; set; }

    [Name(GtfsColumnNames.Friday)]
    public bool Friday { get; set; }

    [Name(GtfsColumnNames.Saturday)]
    public bool Saturday { get; set; }

    [Name(GtfsColumnNames.Sunday)]
    public bool Sunday { get; set; }

    [Name(GtfsColumnNames.StartDate)]
    public DateOnly StartDate { get; set; }

    [Name(GtfsColumnNames.EndDate)]
    public DateOnly EndDate { get; set; }
}
