using System;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Interfaces;
using Volo.Abp.Domain.Entities.Auditing;

namespace Byd.GtfsManagement.Entities.FeedInfos;

// islevi: feed_info.txt veri seti yayimci ve surum bilgilerini saklar.
// sistemdeki gorevi: GTFS veri setinin resmi yayimci, dil, gecerlilik ve iletisim verilerini Feed altinda tutar.
/// <summary>GTFS veri seti yayimci bilgisini temsil eder.</summary>
public class FeedInfo : AuditedAggregateRoot<Guid>, IGtfsEntity
{
    // Kaydin ait oldugu GTFS Feed kimligi.
    public Guid FeedId { get; protected internal set; }

    // Veri setini yayimlayan kurumun tam adi.
    public string PublisherName { get; protected internal set; } = default!;

    // Yayimci kurumun web sitesi adresi.
    public string PublisherUrl { get; protected internal set; } = default!;

    // Veri setinin varsayilan dili (BCP 47).
    public string Language { get; protected internal set; } = default!;

    // Belirtilmediginde kullanilacak varsayilan dil.
    public string? DefaultLanguage { get; protected internal set; }

    // Veri setinin gecerli oldugu baslangic tarihi (YYYYMMDD).
    public string? StartDate { get; protected internal set; }

    // Veri setinin gecerli oldugu bitis tarihi (YYYYMMDD).
    public string? EndDate { get; protected internal set; }

    // Veri setinin yayimci tarafindan belirlenen surum metni.
    public string? Version { get; protected internal set; }

    // Yayimci iletisim e-posta adresi.
    public string? ContactEmail { get; protected internal set; }

    // Yayimci iletisim web sayfasi adresi.
    public string? ContactUrl { get; protected internal set; }

    // Bagli oldugu Feed kaydi.
    public Feed Feed { get; protected internal set; } = default!;

    // EF materialization icin bos kabuk kurar.
    protected FeedInfo()
    {
    }

    // Kaydin degismeyen kimlik ve kapsam bilgilerini kurar.
    public FeedInfo(Guid id, Guid feedId) : base(id)
    {
        FeedId = feedId;
    }
}
