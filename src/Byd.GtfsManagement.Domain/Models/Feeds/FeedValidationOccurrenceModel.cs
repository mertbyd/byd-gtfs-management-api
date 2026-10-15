using System.Text.Json;

namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Hatanin varsa dosya, alan ve CSV satirini, kalan kanonik baglamla tasir.
// sistemdeki gorevi: Degisken notice ayrintilarini saklanan raporda kayipsiz korur.
public class FeedValidationOccurrenceModel
{
    public string? FileName { get; set; }
    public string? FieldName { get; set; }
    public long? CsvRowNumber { get; set; }
    public JsonElement Details { get; set; }
}
