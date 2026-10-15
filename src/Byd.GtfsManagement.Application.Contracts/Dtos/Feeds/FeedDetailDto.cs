namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Feed satirina dogrulayicinin hata, uyari ve bilgi notice'larini ekler.
// sistemdeki gorevi: Yukleme ucu ile tek Feed okuma uclarinin cevabidir; liste satiri bu raporu tasimaz.
public class FeedDetailDto : FeedDto
{
    public FeedValidationReportDto? ValidationReport { get; set; }
}
