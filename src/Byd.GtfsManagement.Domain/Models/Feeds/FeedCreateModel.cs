using System;
using Byd.GtfsManagement.Feeds;

namespace Byd.GtfsManagement.Models.Feeds;

// islevi: Dogrulanmis Feed olusturma girdisini tasir.
// sistemdeki gorevi: Application DTO'sunu Domain katmanindan ayirir.
public class FeedCreateModel
{
    public string? Name { get; set; }
    public string FileName { get; set; } = default!;
    public string? SourceUrl { get; set; }
    public FeedSourceType SourceType { get; set; }
}
