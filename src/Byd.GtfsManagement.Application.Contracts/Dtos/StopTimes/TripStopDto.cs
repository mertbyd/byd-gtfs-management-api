namespace Byd.GtfsManagement.Dtos.StopTimes;

// islevi: Durak ziyaretine ziyaret edilen duragin koordinatlarini ekler.
// sistemdeki gorevi: Sefer ve guzergah detay cevaplarinin durak sirasidir; ziyaretin alanlari StopTimeDto'dan gelir.
public class TripStopDto : StopTimeDto
{
    public double? StopLatitude { get; set; }
    public double? StopLongitude { get; set; }
}
