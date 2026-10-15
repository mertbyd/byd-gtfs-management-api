using System.Text.Json;

namespace Byd.GtfsManagement.Dtos.Feeds;

// islevi: Hatanin varsa dosya, alan ve CSV satirini, kalan kanonik baglamla tasir.
// sistemdeki gorevi: Degisken notice ayrintilarini kayipsiz public raporda korur.
public class FeedValidationOccurrenceDto
{
    public string? FileName { get; set; }
    public string? FieldName { get; set; }
    public long? CsvRowNumber { get; set; }
    public JsonElement Details { get; set; }
}
