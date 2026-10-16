using System;
using System.Collections.Generic;
using Byd.FileManagement.Dtos.StoredFiles;
using Byd.GtfsManagement.Dtos.Feeds;
using Byd.GtfsManagement.Entities.Feeds;
using Byd.GtfsManagement.Models.Feeds;
using Riok.Mapperly.Abstractions;

namespace Byd.GtfsManagement.Mappers.Feeds;

// islevi: Kaynaktan indirilen ZIP'in yukleme girdisine, yukleme girdisinin file modulu istegine, dogrulama raporunun ve
// Feed'in cevaplara eslemelerini uretir.
// sistemdeki gorevi: Application sinirinda manuel alan kopyalamayi onler.
[Mapper]
public partial class FeedMapper
{
    public partial UploadFeedDto Map(ImportFeedDto input, string fileName, byte[] data);
    [MapperIgnoreTarget(nameof(FeedCreateModel.SourceType))]
    public partial FeedCreateModel Map(UploadFeedDto input, string? sourceUrl);
    public partial UploadStoredFileDto Map(UploadFeedDto input, Guid externalId, Guid categoryId);
    public partial FeedValidationReportModel Map(FeedValidationReportDto report);
    public partial FeedValidationReportDto Map(FeedValidationReportModel report);
    public partial FeedDto Map(Feed entity);
    public partial List<FeedDto> Map(List<Feed> entities);
    public partial FeedDetailDto MapToDetail(Feed entity);
    public partial FeedSummaryDto Map(FeedSummaryModel model);
}
