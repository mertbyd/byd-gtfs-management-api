using System;
using Byd.GtfsManagement.Feeds;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Surumun ust Feed'ini, yayin secimini, ZIP dosyasini ve entegrasyon durumunu tasir.
// sistemdeki gorevi: Feed listesinin satiridir; liste ayni zamanda yukleme gecmisidir. Agir dogrulama raporu ayrinti
// cevabinda doner.
public class FeedDto
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Keys / Scope
    public Guid? OrganizationId { get; set; }
    public Guid StoredFileId { get; set; }

    // Table Columns / Data
    public string StoredFileName { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? SourceUrl { get; set; }
    public FeedSourceType SourceType { get; set; }
    public bool IsActive { get; set; }
    public FeedStatus Status { get; set; }
    public string? FailureCode { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? LastModificationTime { get; set; }
}
