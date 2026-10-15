namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Indirilecek dosyanin adini, icerik turunu ve icerigini tasir.
// sistemdeki gorevi: Feed'den disa aktarilan GTFS ZIP veya dogrulama raporunun cevabidir; icerik JSON'da base64 gider.
public class FeedFileDto
{
    public string FileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public byte[] Data { get; set; } = [];
}
