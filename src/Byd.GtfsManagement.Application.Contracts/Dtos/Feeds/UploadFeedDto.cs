namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Feed adina ek olarak yuklenen GTFS ZIP'inin dosya adini ve byte'larini tasir.
// sistemdeki gorevi: Yukleme ucunun girdisidir; file modulu gibi ham byte alir, IFormFile almaz. Kaynak servisten cekilen ZIP
// de ayni girdiyle ayni akisa girer.
public class UploadFeedDto : FeedNameInputDto
{
    public string FileName { get; set; } = default!;
    public byte[] Data { get; set; } = default!;
}
