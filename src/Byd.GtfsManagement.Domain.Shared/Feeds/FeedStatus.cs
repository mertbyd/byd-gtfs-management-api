namespace Byd.GtfsManagement.Feeds;

// islevi: Bir Feed'in ZIP yuklemesinden veritabanina entegrasyona kadar gectigi durumlari tanimlar.
// sistemdeki gorevi: Yukleme ucu, entegrasyon kuyrugu, aktiflestirme ve Feed listesi ayni durum sozlesmesini okur.
public enum FeedStatus
{
    // ZIP file modulune kaydedildi, dogrulama sonucu henuz yazilmadi.
    Uploaded = 1,

    // Dogrulayici hata buldu; GTFS tablolarina veri yazilmaz.
    ValidationFailed = 2,

    // Dogrulamadan gecti; entegrasyon kuyrugunda bekliyor.
    Validated = 3,

    // Tum GTFS tablolari bu Feed'e yazildi; artik aktiflestirilebilir.
    Integrated = 4,

    // Entegrasyon patladi; yazilan her sey geri alindi.
    IntegrationFailed = 5
}
