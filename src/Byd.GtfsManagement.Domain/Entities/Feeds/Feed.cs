using System;
using Byd.CoreExtensions.Interfaces;
using Byd.GtfsManagement.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Byd.GtfsManagement.Entities.Feeds;

// islevi: Bir organizasyonun GTFS arsivini, kaynagini ve islem durumunu saklar.
// sistemdeki gorevi: GTFS satirlarinin kapsam ve surum kokudur; is kurallari FeedManager'dadir.
public class Feed : AuditedAggregateRoot<Guid>, IMultiTenant, IMultiOrganization
{
    // Arsivin ait oldugu tenant.
    public Guid? TenantId { get; internal set; }
    // Arsivin ait oldugu organizasyon.
    public Guid? OrganizationId { get; internal set; }
    // File modulundeki arsivin kimligi.
    public Guid StoredFileId { get; internal set; }
    // Son islemi baslatan kullanici; sonuc bildirimi icin kullanilir.
    public Guid? LastOperationUserId { get; internal set; }
    // File modulunde saklanan arsivin dosya adi.
    public string StoredFileName { get; internal set; } = default!;
    // Kullanicinin Feed'e verdigi ad.
    public string Name { get; internal set; } = default!;
    // URL kaynagi varsa arsivin alindigi adres.
    public string? SourceUrl { get; internal set; }
    // Arsivin yukleme veya URL yoluyla geldigi kaynak; veritabaninda adiyla saklanir.
    public FeedSourceType SourceType { get; internal set; }
    // Dogrulama ve entegrasyon yasam dongusu; veritabaninda adiyla saklanir.
    public FeedStatus Status { get; internal set; }
    // Organizasyonda sorgulara acik olan Feed'i belirtir.
    public bool IsActive { get; internal set; }
    // Son basarisiz islemin lokalize hata kodu.
    public string? FailureCode { get; internal set; }
    // GTFS dogrulama sonucu; rapor ayrinti ucunda kullanilir.
    public FeedValidationReportModel? ValidationReport { get; internal set; }

    // EF materialization icin bos kabuk kurar.
    protected Feed()
    {
    }

    // Yeni arsivin kapsam ve kaynak bilgisini atar.
    public Feed(Guid id, Guid tenantId, Guid organizationId, string name, FeedSourceType sourceType,
        string? sourceUrl, Guid? operationUserId = null) : base(id)
    {
        TenantId = tenantId;
        OrganizationId = organizationId;
        Name = name;
        SourceType = sourceType;
        SourceUrl = sourceUrl;
        Status = FeedStatus.Uploaded;
        IsActive = false;
        LastOperationUserId = operationUserId;
    }
}
