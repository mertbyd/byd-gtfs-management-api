using System;
using Byd.GtfsManagement.Entities.Feeds;
using Volo.Abp.Domain.Entities;

namespace Byd.GtfsManagement.Interfaces;

// islevi: Feed kapsamli tum GTFS varliklarinin ortak sozlesmesini tanimlar.
// sistemdeki gorevi: Kimlik ve Feed bagini tek sozlesmede birlestirir; tenant ve organizasyon Feed'den belirlenir. Kayitlar bu sozlesme
// uzerinden damgalayip toplu yazar. Uygulayan entity'ler setter'larini protected internal acar: Manager'lar ayni assembly'den,
// ice aktarim mapper'i derleme zamaninda uretilen erisimcilerle yazar; Application kodu dogrudan yazamaz.
public interface IGtfsEntity : IEntity<Guid>
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    Guid FeedId { get; }

    // Kaydin bagli oldugu GTFS Feed navigasyonu.
    Feed Feed { get; }
}
