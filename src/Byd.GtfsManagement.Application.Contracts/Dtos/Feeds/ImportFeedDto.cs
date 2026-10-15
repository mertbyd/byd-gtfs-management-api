namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: URL kaynagindan olusacak Feed'in adresini ve opsiyonel adini tasir.
// sistemdeki gorevi: Kaynak ZIP'i indirildikten sonra yukleme akisi yeni pasif Feed acar.
public class ImportFeedDto : FeedNameInputDto
{
    public string? Url { get; set; }
}
