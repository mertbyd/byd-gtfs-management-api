namespace Byd.GtfsManagement.Journeys;

// islevi: Bir yolculuk adiminin nasil kat edildigini bildirir.
// sistemdeki gorevi: Planlayicinin urettigi adimlar ve public cevap ayni degerleri kullanir.
public enum JourneyLegKind
{
    // Bir seferle bir duraktan digerine gidilir.
    Transit = 1,

    // Yakin duraklar arasinda sokakta yurunur.
    Walk = 2,

    // Istasyon icinde pathways.txt ile tanimli yoldan gecilir.
    Pathway = 3,

    // transfers.txt ile tanimli aktarma noktasinda arac degistirilir.
    Transfer = 4
}
