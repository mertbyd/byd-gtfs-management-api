namespace Byd.GtfsManagement.Feeds;

// islevi: GTFS dosyasindaki bir anahtari veya lookup kodunu, baktigi tablonun adiyla birlikte tasir.
// sistemdeki gorevi: CSV hucresi kayit veya lookup kimligine cevrilirken kullanilir; ayni deger farkli tablolarda farkli
// kayitlari gosterebildigi icin deger hedef tablosundan ayrilmaz.
public readonly record struct GtfsKey(string Target, string Value);
